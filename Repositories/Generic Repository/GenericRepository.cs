using BusinessObjects;
using BusinessObjects.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbSet
                    .Include("Category")
                    .ToListAsync();
            }
            else if (typeof(T) == typeof(Category))
            {
                return await _dbSet
                    .Include("Products")
                    .ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _dbSet
                    .Include("Category")
                    .FirstOrDefaultAsync(product => (product as Product).ProductId == id);
            }
            else if (typeof(T) == typeof(Category))
            {
                return await _dbSet
                    .Include("Products")
                    .FirstOrDefaultAsync(category => (category as Category).CategoryId == id);
            }

            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> SearchAsync(SearchQuery query)
        {
            IQueryable<T> items = _dbSet;
            IQueryable<T> result = _dbSet;
            foreach (var filter in query.Filters)
            {
                var property = typeof(T).GetProperty(filter.Key);
                if (property != null && filter.Value != null)
                {
                    result = result.Where(e => EF.Property<object>(e, filter.Key).ToString().Contains(filter.Value.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                result = query.SortOrder.ToLower() == "desc"
                    ? result.OrderByDescending(e => EF.Property<object>(e, query.SortBy))
                    : result.OrderBy(e => EF.Property<object>(e, query.SortBy));
            }

            result = result.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);
            foreach (var filter in query.Filters)
            {
                var propertyInfo = typeof(T).GetProperty(filter.Key, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "e");
                    var property = Expression.Property(parameter, propertyInfo);
                    var constant = Expression.Constant(filter.Value);
                    var equalExpression = Expression.Equal(property, constant);
                    var lambda = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);
                    items = items.Where(lambda);
                }
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                var searchProperty = typeof(T).GetProperty("ProductName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (searchProperty != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "e");
                    var property = Expression.Property(parameter, searchProperty);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var searchExpression = Expression.Call(property, containsMethod, Expression.Constant(query.SearchTerm));
                    var lambda = Expression.Lambda<Func<T, bool>>(searchExpression, parameter);
                    items = items.Where(lambda);
                }
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var sortProperty = typeof(T).GetProperty(query.SortBy, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (sortProperty != null)
                {
                    items = query.SortOrder.ToLower() == "desc"
                        ? items.OrderByDescending(e => EF.Property<object>(e, query.SortBy))
                        : items.OrderBy(e => EF.Property<object>(e, query.SortBy));
                }
            }

            items = items.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);

            return await items.ToListAsync();
        }
    }

}
