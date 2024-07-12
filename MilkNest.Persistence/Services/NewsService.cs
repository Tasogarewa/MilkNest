using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.News.Commands.CreateNews;
using MilkNest.Application.CQRS.News.Commands.DeleteNews;
using MilkNest.Application.CQRS.News.Commands.UpdateNews;
using MilkNest.Application.CQRS.News.Queries.GetNews;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public NewsService(IRepository<News> newsRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> CreateNewsAsync(CreateNewsCommand createNews)
        {
            List<Image> images = new List<Image>();
            foreach (var image in createNews.Images)
            {
                images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(image) });
            }
            var News = await _newsRepository.CreateAsync(new News() { Content = createNews.Content, Title = createNews.Title, Images = images, PublishDate = DateTime.Now });
            return News.Id;
        }

        public async Task<Unit> DeleteNewsAsync(DeleteNewsCommand deleteNews)
        {
            var News = await _newsRepository.GetAsync(deleteNews.Id);
            if (News != null)
            {
                await _newsRepository.DeleteAsync(deleteNews.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(News, deleteNews.Id);
                return Unit.Value;
            }
        }

        public async Task<NewsListVm> GetNewsAsync(GetNewsQuery getNews)
        {
            var News = await _newsRepository.GetAllAsync();
            if (News != null)
            {
                var newsDtos = _mapper.ProjectTo<NewsDto>(News.AsQueryable()).ToList();
                return new NewsListVm() {  NewsDtos = newsDtos };
            }
            else
            {
                NotFoundException.ThrowRange(News);
                return null;
            }
        }

        public async Task<NewsVm> GetSingleNewsAsync(GetSingleNewsQuery getSingleNews)
        {
            var News = await _newsRepository.GetAsync(getSingleNews.Id);
            if (News != null)
            {
                return _mapper.Map<NewsVm>(News);
            }
            else
            {
                NotFoundException.Throw(News, getSingleNews.Id);
                return null;
            }
        }
        public async Task<Guid> UpdateNewsAsync(UpdateNewsCommand updateNews)
        {
            var News = await _newsRepository.GetAsync(updateNews.Id);
            List<Image> NewPhotos = new List<Image>();
            if (News != null)
            {
                foreach (var img in News.Images)
                {
                    _fileStorageService.DeleteFile(img.Url);
                }
                foreach (var img in updateNews.Images)
                {
                    News.Images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(img) });
                }
                News.UpdateDate = DateTime.Now;
                News.Title = updateNews.Title;
                News.Content = updateNews.Content;
                await _newsRepository.UpdateAsync(News);
                return News.Id;
            }
            else
            {
                NotFoundException.Throw(News, updateNews.Id);
                return Guid.Empty;
            }
        }
    }
}
