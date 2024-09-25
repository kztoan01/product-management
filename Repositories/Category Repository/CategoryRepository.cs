using BusinessObjects.Objects;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Category_Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> SearchAsync(CategoryQuery query)
        {
            IQueryable<Category> categories = _dbSet;

            if (!string.IsNullOrEmpty(query.CategoryName))
            {
                categories = categories.Where(c => c.CategoryName.Contains(query.CategoryName));
            }

            return await categories.ToListAsync();
        }
    }

}
