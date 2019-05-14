using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain.Logic.Interface;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;

namespace Wanaheim.Core.Domain.Logic.Concret
{
    public class ItemLogic : IItemLogic
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _repository;

        public ItemLogic(IMapper mapper, IItemRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ItemDtoReadOnly> Add(ItemDto item)
        {
            var itemInDb = _mapper.Map<ItemDto, Item>(item);
            itemInDb.CreationDate = DateTime.Now;

            _repository.Add(itemInDb);
            await _unitOfWork.Complete();

            var model = await _repository.Get(i => i.Id == itemInDb.Id);

            return _mapper.Map<Item, ItemDtoReadOnly>(model);
        }

        public async Task<bool> Delete(int id)
        {
            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return false;
            }

            _repository.Remove(itemInDb);
            await _unitOfWork.Complete();

            return true;
        }

        public async Task<QueryResultDto<ItemDtoReadOnly>> Get(ItemsQueryDto queryDto)
        {
            var query = _mapper.Map<ItemsQueryDto, ItemsQuery>(queryDto);
            var model = await _repository.GetFiltrated(query);

            return _mapper.Map<QueryResult<Item>, QueryResultDto<ItemDtoReadOnly>>(model);
        }

        public async Task<ItemDtoReadOnly> Get(int id)
        {
            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return null;
            }

            return _mapper.Map<Item, ItemDtoReadOnly>(itemInDb);
        }

        public async Task<ItemDtoReadOnly> Update(int id, ItemDto item)
        {
            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return null;
            }

            _mapper.Map(item, itemInDb);
            itemInDb.CreationDate = DateTime.Now;

            await _unitOfWork.Complete();

            itemInDb = await _repository.Get(i => i.Id == id);

            return _mapper.Map<Item, ItemDtoReadOnly>(itemInDb);
        }
    }
}
