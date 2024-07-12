using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class NewsListVm
    {
        public List<NewsDto> NewsDtos { get; set; }
    }
}
