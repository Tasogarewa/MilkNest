using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class GetSingleNewsQuery:IRequest<NewsVm>
    {
        public Guid Id {  get; set; }
    }
}
