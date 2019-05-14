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
using Wanaheim.Core.Domain.Logic.Interface;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;

namespace FloatingMarket.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("/api/items")]
    public class ItemsController : Controller
    {
        private readonly IItemLogic _itemLogic;

        public ItemsController(IItemLogic itemLogic)
        {
            _itemLogic = itemLogic;
        }

        [HttpGet]
        public async Task<QueryResultDto<ItemDtoReadOnly>> GetItems(ItemsQueryDto queryDto)
        {
            return await _itemLogic.Get(queryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _itemLogic.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _itemLogic.Add(item);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemInDb = await _itemLogic.Update(id, item);

            if (itemInDb == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var ifDeleted = await _itemLogic.Delete(id);

            if (!ifDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}