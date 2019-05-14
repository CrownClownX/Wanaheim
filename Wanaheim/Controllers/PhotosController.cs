using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;
using Wanaheim.Services;

namespace FloatingMarket.Controllers
{
    [Route("/api/items/{itemId}/Photos")]
    public class PhotosController : Controller
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _repository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoService _photoService;

        public PhotosController(IMapper mapper, IItemRepository repository, IUnitOfWork unitOfWork, 
            IPhotoRepository photoRepository, IPhotoService photoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoRepository = photoRepository;
            _photoService = photoService;
            _repository = repository;
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotos(int itemId)
        {
            var photos = await _photoRepository.GetPhotosByItem(itemId);

            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int itemId, IFormFile file)
        {
            var item = await _repository.Get(i => i.Id == itemId);

            if (item == null)
                return NotFound();
            if (file == null)
                return BadRequest("No file");

            var photo = await _photoService.UploadPhoto(file);

            if (photo == null) //Add PhotoValidationState bind to string with error message 
                return BadRequest("Empty file");

            item.Photos.Add(photo);
            await _unitOfWork.Complete();

            return Ok(_mapper.Map<Photo, PhotoDto>(photo));
        }
    }
}