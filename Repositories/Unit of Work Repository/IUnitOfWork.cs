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
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task<int> SaveChangesAsync();
    }
}
