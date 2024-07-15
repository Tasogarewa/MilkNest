using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class GetSingleNewsQueryHandler : IRequestHandler<GetSingleNewsQuery, NewsVm>
    {
        private readonly INewsService _newsService;

        public GetSingleNewsQueryHandler(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<NewsVm> Handle(GetSingleNewsQuery request, CancellationToken cancellationToken)
        {
            return await _newsService.GetSingleNewsAsync(request);
        }
    }
}
