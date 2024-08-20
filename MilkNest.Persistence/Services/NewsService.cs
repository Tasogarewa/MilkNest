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
        private readonly ITranslationService _translationService;

        public NewsService(IRepository<News> newsRepository, IMapper mapper, IFileStorageService fileStorageService, ITranslationService translationService)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _translationService = translationService;
        }

        public async Task<Guid> CreateNewsAsync(CreateNewsCommand createNews)
        {
            List<Image> images = new List<Image>();
            foreach (var image in createNews.Images)
            {
                images.Add(new Image() { Url = await _fileStorageService.SaveFileAsync(image) });
            }
            var Localizations = await _translationService.FillLocalizations<NewsLocalization>(createNews.Description, createNews.Title);
            var News = await _newsRepository.CreateAsync(new News() { Localizations = Localizations,  Images = images, PublishDate = DateTime.Now });
            return News.Id;
        }

        public async Task<Unit> DeleteNewsAsync(DeleteNewsCommand deleteNews)
        {
            var news = await _newsRepository.GetAsync(deleteNews.Id);
            if (news != null)
            {
                var imagesToDelete = new List<Image>(news.Images);

                foreach (var image in imagesToDelete)
                {
                    await _fileStorageService.DeleteImageAsync(image.Id);
                }
                news.Images.Clear();
                await _newsRepository.DeleteAsync(deleteNews.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(news, deleteNews.Id);
                return Unit.Value;
            }
        }

        public async Task<NewsListVm> GetNewsAsync(GetNewsQuery getNews)
        {
            var newsItems = await _newsRepository.GetAllAsync();

            if (newsItems != null)
            {
                var newsDtos = _mapper.Map<List<NewsDto>>(newsItems);

                foreach (var newsDto in newsDtos)
                {
                    newsDto.Language = await _translationService.GetCurrentLanguageAsync();

                    var localization = newsItems.FirstOrDefault(n => n.Id == newsDto.Id)?
                                       .Localizations.FirstOrDefault(l => l.Language == newsDto.Language);
                    if (localization != null)
                    {
                        newsDto.Title = localization.Title;
                    }
                }

                return new NewsListVm() { NewsDtos = newsDtos };
            }
            else
            {
                NotFoundException.ThrowRange(newsItems);
                return null;
            }
        }
        public async Task<NewsVm> GetSingleNewsAsync(GetSingleNewsQuery getSingleNews)
        {
            var newsItem = await _newsRepository.GetAsync(getSingleNews.Id);
            if (newsItem != null)
            {
                var mappedSingleNews = _mapper.Map<NewsVm>(newsItem);

                mappedSingleNews.Language = await _translationService.GetCurrentLanguageAsync();

                var localization = newsItem.Localizations.FirstOrDefault(l => l.Language == mappedSingleNews.Language);
                if (localization != null)
                {
                    mappedSingleNews.Title = localization.Title;
                    mappedSingleNews.Description = localization.Description;
                }

                return mappedSingleNews;
            }
            else
            {
                NotFoundException.Throw(newsItem, getSingleNews.Id);
                return null;
            }
        }
        public async Task<Guid> UpdateNewsAsync(UpdateNewsCommand updateNews)
        {
            var news = await _newsRepository.GetAsync(updateNews.Id);
            if (news != null)
            {
                List<Image> imagesToDelete = new List<Image>(news.Images);
                foreach (var img in imagesToDelete)
                {
                    await _fileStorageService.DeleteImageAsync(img.Id);
                }
                news.Images.Clear();
                foreach (var img in updateNews.Images)
                {
                    var newImage = new Image() { Url = await _fileStorageService.SaveFileAsync(img) };
                    news.Images.Add(newImage);
                }

                news.UpdateDate = DateTime.Now;
                news.Localizations.Clear();
                news.Localizations = await _translationService.FillLocalizations<NewsLocalization>(updateNews.Description,updateNews.Title);

                await _newsRepository.UpdateAsync(news);
                return news.Id;
            }
            else
            {
                NotFoundException.Throw(news, updateNews.Id);
                return Guid.Empty;
            }
        }
    }
    }

