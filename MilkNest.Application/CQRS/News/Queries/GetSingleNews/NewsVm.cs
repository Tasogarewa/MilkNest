using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class NewsVm:IMapWith<MilkNest.Domain.News>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public List<Image> Images { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.News, NewsDto>().  
                 ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title)).
                  ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content)).
                   ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate)).
                    ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images));
        }
    }
}
