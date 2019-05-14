using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Services
{
    public interface IStorageService
    {
        Task<string> StoreFile(string uploadsFolderPath, IFormFile file);
        void DeleteFile(string path);
    }
}
