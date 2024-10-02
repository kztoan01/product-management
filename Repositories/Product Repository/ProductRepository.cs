using BusinessObjects.Objects;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Product_Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductResponse>> SearchAsync(ProductQuery query)
        {
            IQueryable<Product> products = _dbSet.Include(p => p.Category);

            // Filtering logic
            if (!string.IsNullOrEmpty(query.ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(query.ProductName));
            }

            if (query.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == query.CategoryId.Value);
            }

            if (query.MinPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                products = products.Where(p => p.UnitPrice <= query.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                products = query.SortOrder.ToLower() == "desc"
                    ? products.OrderByDescending(e => EF.Property<object>(e, query.SortBy))
                    : products.OrderBy(e => EF.Property<object>(e, query.SortBy));
            }

            products = products.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);

            var productDtos = await products
                .Select(p => new ProductResponse
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    UnitsInStock = p.UnitsInStock,
                    UnitPrice = p.UnitPrice
                })
                .ToListAsync();

            return productDtos;
        }


    }

}
