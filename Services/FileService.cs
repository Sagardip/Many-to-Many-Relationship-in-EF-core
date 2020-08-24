using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace one.Services
{

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment env;
        public FileService(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public string Upload(IFormFile file)
        {
            var uploadDirecotroy = "uploads/";
            var uploadPath = Path.Combine(env.WebRootPath, uploadDirecotroy);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }
            return fileName;
        }
    }
}