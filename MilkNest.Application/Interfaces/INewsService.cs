using MediatR;
using MilkNest.Application.CQRS.News.Commands.CreateNews;
using MilkNest.Application.CQRS.News.Commands.DeleteNews;
using MilkNest.Application.CQRS.News.Commands.UpdateNews;
using MilkNest.Application.CQRS.News.Queries.GetNews;
using MilkNest.Application.CQRS.Product.Commands.CreateProduct;
using MilkNest.Application.CQRS.Product.Commands.DeleteProduct;
using MilkNest.Application.CQRS.Product.Commands.UpdateProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface INewsService
    {
        public Task<Guid> CreateNewsAsync(CreateNewsCommand createNews);
        public Task<Unit> DeleteNewsAsync(DeleteNewsCommand deleteNews);
        public Task<NewsVm> GetSingleNewsAsync(GetSingleNewsQuery getSingleNews);
        public Task<Guid> UpdateNewsAsync(UpdateNewsCommand updateNews);
        public Task<NewsListVm> GetNewsAsync(GetNewsQuery getNews);
    }
}
