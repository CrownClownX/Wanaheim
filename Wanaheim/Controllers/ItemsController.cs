using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;

namespace FloatingMarket.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("/api/items")]
    public class ItemsController : Controller
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _repository;

        public ItemsController(IMapper mapper, IItemRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpGet]
        public async Task<QueryResultDto<ItemDtoReadOnly>> GetItems(ItemsQueryDto queryDto)
        {
            var query = _mapper.Map<ItemsQueryDto, ItemsQuery>(queryDto);
            var model = await _repository.GetFiltrated(query);

            return _mapper.Map<QueryResult<Item>, QueryResultDto<ItemDtoReadOnly>>(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Item, ItemDtoReadOnly>(itemInDb));
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemInDb = _mapper.Map<ItemDto, Item>(item);
            itemInDb.CreationDate = DateTime.Now;

            _repository.Add(itemInDb);
            await _unitOfWork.Complete();

            var model = await _repository.Get(i => i.Id == itemInDb.Id);

            return Ok(_mapper.Map<Item, ItemDtoReadOnly>(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(item, itemInDb);
            itemInDb.CreationDate = DateTime.Now;

            await _unitOfWork.Complete();

            itemInDb = await _repository.Get(i => i.Id == id);

            return Ok(_mapper.Map<Item, ItemDtoReadOnly>(itemInDb));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var itemInDb = await _repository.Get(i => i.Id == id);

            if (itemInDb == null)
            {
                return NotFound();
            }

            _repository.Remove(itemInDb);
            await _unitOfWork.Complete();

            return Ok();
        }
    }
}