using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Services.Settings;

namespace Wanaheim.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IStorageService _storageManager;
        private readonly IHostingEnvironment _host;
        private readonly PhotoSettings _settings;
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IStorageService storageManager, IHostingEnvironment host, 
            IOptionsSnapshot<PhotoSettings> options, IMapper mapper, IUnitOfWork unitOfWork,
            IPhotoRepository photoRepository)
        {
            _storageManager = storageManager;
            _host = host;
            _settings = options.Value;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoRepository = photoRepository;
        }

        public async Task<Photo> UploadPhoto(IFormFile file)
        {
            if (!IfPicterIsValid(file))
            {
                return null;
            }

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var fileName = await _storageManager.StoreFile(uploadsFolderPath, file);

            var photo = new Photo()
            {
                FileName = fileName
            };

            return photo;
        }

        public void DeletePhoto(string path)
        {
            _storageManager.DeleteFile(path);
        }

        private bool IfPicterIsValid(IFormFile file)
        {
            if (file.Length == 0)
                return false; // BadRequest("Empty file");
            if (file.Length > _settings.MaxBytes)
                return false; // BadRequest("File is to large");
            if (!_settings.IsSupported(file.FileName))
                return false; // BadRequest("Inavlid type");

            return true;
        }
    }
}
