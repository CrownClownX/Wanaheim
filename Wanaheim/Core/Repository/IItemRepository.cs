using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;

namespace Wanaheim.Core.Repository
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<QueryResult<Item>> GetFiltrated(ItemsQuery query);
    }
}