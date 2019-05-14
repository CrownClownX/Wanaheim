using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;

namespace Wanaheim.Services
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(IFormFile file);
        void DeletePhoto(string path);
    }
}
