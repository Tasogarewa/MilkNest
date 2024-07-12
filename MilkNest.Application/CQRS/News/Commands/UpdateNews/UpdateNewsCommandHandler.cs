using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler:IRequestHandler<UpdateNewsCommand,Guid>
    {
        private readonly INewsService _newsService;

        public UpdateNewsCommandHandler(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<Guid> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            return await _newsService.UpdateNewsAsync(request);
        }
    }
}
