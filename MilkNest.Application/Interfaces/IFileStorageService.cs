using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilkNest.Domain;


namespace MilkNest.Application.Interfaces
{
    public interface IFileStorageService
    {

        Task<string> SaveFileAsync(IFormFile file);
        public  Task<string> UpdateFileAsync(IFormFile newFile, string oldFile);
        void DeleteFile(string filePath);
        Task<Guid> CreateImageAsync(string imageUrl);
        Task<bool> DeleteImageAsync(Guid imageId);
        Task<bool> UpdateImageAsync(Guid imageId, string imageUrl);
        Task<Image> GetImageAsync(Guid imageId);
        Task<List<Image>> GetImagesAsync();
    }
}
