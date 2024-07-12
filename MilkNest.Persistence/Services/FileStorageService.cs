using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MilkNest.Persistence.Services.FileStorageService;

namespace MilkNest.Persistence.Services
{
    
        public class FileStorageService : IFileStorageService
        {
            private readonly   IHostEnvironment _env;

            public FileStorageService(IHostEnvironment env)
            {
                _env = env;
            }

            public async Task<string> SaveFileAsync(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty.");

                var uploadPath = Path.Combine(_env.ContentRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"/uploads/{file.FileName}";
                return fileUrl;
            }

          
            public async Task<string> UpdateFileAsync(IFormFile newFile,string oldFile)
            {
                if (newFile == null || newFile.Length == 0)
                    throw new ArgumentException("New file is empty.");
                DeleteFile(oldFile);
                return await SaveFileAsync(newFile);
            }
        

            public void DeleteFile(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentException("File path cannot be empty.");

                var fullPath = Path.Combine(_env.ContentRootPath, filePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }
    
}
