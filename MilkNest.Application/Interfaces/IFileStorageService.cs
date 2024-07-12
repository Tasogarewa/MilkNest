using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IFileStorageService
    {

        Task<string> SaveFileAsync(IFormFile file);
        public  Task<string> UpdateFileAsync(IFormFile newFile, string oldFile);
        void DeleteFile(string filePath);
    }
}
