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

namespace FloatingMarket.Controllers
{
    [Route("/api/items/{itemId}/Photos")]
    public class PhotosController : Controller
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _repository;
        private IHostingEnvironment _host;
        private readonly PhotoSettings _settings;
        private readonly IPhotoRepository _photoRepository;

        public PhotosController(IHostingEnvironment host, IMapper mapper, IItemRepository repository, IUnitOfWork unitOfWork, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            _host = host;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoRepository = photoRepository;
            _repository = repository;
            _settings = options.Value;
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
            if (file.Length == 0)
                return BadRequest("Empty file");
            if (file.Length > _settings.MaxBytes)
                return BadRequest("File is to large");
            if (!_settings.IsSupported(file.FileName))
                return BadRequest("Inavlid type");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo() { FileName = fileName };
            item.Photos.Add(photo);
            await _unitOfWork.Complete();

            return Ok(_mapper.Map<Photo, PhotoDto>(photo));
        }
    }
}