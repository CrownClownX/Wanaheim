using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;

namespace Wanaheim.Persistence.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        private ApplicationDbContext _marketContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .ToListAsync();
        }

        public override async Task<Category> Get(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .SingleOrDefaultAsync(predicate);
        }

    }
}
