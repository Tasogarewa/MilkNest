using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, NewsListVm>
    {
        private readonly INewsService _newsService;

        public GetNewsQueryHandler(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<NewsListVm> Handle(GetNewsQuery request, CancellationToken cancellationToken)
        {
            return await _newsService.GetNewsAsync(request);
        }
    }
}
