using BusinessObjects;
using Repositories.Category_Repository;
using Repositories.Generic_Repository;
using Repositories.Product_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Unit_of_Work_Repository
{

    public class UnitOfWork : IUnitOfWork
    {

        private IProductRepository _products;
        private ICategoryRepository _categories;
        private readonly MyDbContext _context;

        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }

        public IProductRepository Products
        {
            get { return _products ??= new ProductRepository(_context); }
        }

        public ICategoryRepository Categories
        {
            get { return _categories ??= new CategoryRepository(_context); }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
