using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Helpers;

namespace Wanaheim.Persistence.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }

        private ApplicationDbContext _marketContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public override async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items
                .Include(i => i.Subcategory)
                    .ThenInclude(s => s.Category)
                .ToListAsync();
        }

        public override async Task<Item> Get(Expression<Func<Item, bool>> predicate)
        {
            return await _context.Items
                .Include(i => i.Subcategory)
                    .ThenInclude(s => s.Category)
                .SingleOrDefaultAsync(predicate);
        }

        public async Task<QueryResult<Item>> GetFiltrated(ItemsQuery query)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Item, object>>>()
            {
                ["name"] = i => i.Name,
                ["price"] = i => i.Price,
                ["subcategory"] = i => i.SubcategoryId
            };

            var items = _context.Items
                .Include(i => i.Subcategory)
                    .ThenInclude(s => s.Category)
                .AsQueryable();

            if (query.CategoryId.HasValue)
            {
                items = items.Where(i => i.Subcategory.CategoryId == query.CategoryId)
                    .AsQueryable();
            }

            var count = await items.CountAsync();

            items = items.NullableOrderBy(columnsMap, query)
                .ApplingPagination(query);

            QueryResult<Item> result = new QueryResult<Item>()
            {
                Entities = await items.ToListAsync(),
                Count = count
            };

            return result;
        }
    }
}
