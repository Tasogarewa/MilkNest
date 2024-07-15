using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MilkNest.Persistence.Services.FileStorageService;
using static System.Net.Mime.MediaTypeNames;

namespace MilkNest.Persistence.Services
{
    
        public class FileStorageService : IFileStorageService
        {
            private readonly   IHostEnvironment _env;
        private readonly IRepository<Domain.Image> _imageRepository;
        private readonly IMapper _mapper;
       

        public FileStorageService(IHostEnvironment env, IRepository<Domain.Image> imageRepository, IMapper mapper)
        {
            _env = env;
            _imageRepository = imageRepository;
            _mapper = mapper;
        
        }
        public async Task<Guid> CreateImageAsync(string imageUrl)
        {
            var image = new Domain.Image { Url = imageUrl };
            await _imageRepository.CreateAsync(image);
            return image.Id;
        }

        public async Task<bool> DeleteImageAsync(Guid imageId)
        {
            var image = await _imageRepository.GetAsync(imageId);
            if (image != null)
            {
                DeleteFile(image.Url);
                await _imageRepository.DeleteAsync(imageId);
                return true;
            }
            else
            {
                NotFoundException.Throw(image, imageId);
                return false;
            }
        }

        public async Task<bool> UpdateImageAsync(Guid imageId, string imageUrl)
        {
            var image = await _imageRepository.GetAsync(imageId);
            if (image != null)
            {
                image.Url = imageUrl;
                await _imageRepository.UpdateAsync(image);
                return true;
            }
            else
            {
                NotFoundException.Throw(image, imageId);
                return false;
            }
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

        public async Task<Domain.Image> GetImageAsync(Guid imageId)
        {
                var image = await _imageRepository.GetAsync(imageId);
                if (image != null)
                {
                    return image;
                }
                else
                {
                    NotFoundException.Throw(image, imageId);
                    return null;
                }
            
        }

        public async Task<List<Domain.Image>> GetImagesAsync()
        {
            var images = await _imageRepository.GetAllAsync();
            if (images != null)
            {

                return images.ToList();
            }
            else
            {
                NotFoundException.ThrowRange(images);
                return null;
            }
        }
    }
    
}
