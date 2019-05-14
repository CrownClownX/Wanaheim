using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Mapping.Dtos;

namespace Wanaheim.Core.Domain.Logic.Interface
{
    public interface IItemLogic
    {
        Task<QueryResultDto<ItemDtoReadOnly>> Get(ItemsQueryDto queryDto);
        Task<ItemDtoReadOnly> Get(int id);
        Task<ItemDtoReadOnly> Add(ItemDto item);
        Task<ItemDtoReadOnly> Update(int id, ItemDto item);
        Task<bool> Delete(int id);
    }
}
