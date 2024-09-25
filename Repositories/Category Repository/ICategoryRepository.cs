using BusinessObjects.Objects;
using BusinessObjects;
using Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Category_Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> SearchAsync(CategoryQuery query);
    }

}
