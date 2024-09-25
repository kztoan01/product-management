using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            } else if (typeof(T) == typeof(Category))
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

    }

}
