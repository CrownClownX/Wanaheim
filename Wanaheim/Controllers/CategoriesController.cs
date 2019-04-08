using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;

namespace FloatingMarket.Controllers
{
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _repository;

        public CategoriesController(IMapper mapper, ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var model = await _repository.GetAll();

            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var model = await _repository.Get(c => c.Id == id);

            if(model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Category, CategoryDto>(model));
        }
    }
}