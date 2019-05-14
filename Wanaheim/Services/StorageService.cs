using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Wanaheim.Services
{
    public class StorageService : IStorageService
    {
        public async Task<string> StoreFile(string uploadsFolderPath, IFormFile file)
        {
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var name = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(uploadsFolderPath, name);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return name;
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
