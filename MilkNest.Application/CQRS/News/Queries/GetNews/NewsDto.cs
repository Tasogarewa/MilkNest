using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class NewsDto:IMapWith<MilkNest.Domain.News>
    {

       
        public NewsDto()
        {
        }

     
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public  List<string> Images { get; set; }
        public string Language { get; set; }
        public  void Mapping(Profile profile)
        {
           
            profile.CreateMap<MilkNest.Domain.News, NewsDto>().
                 ForMember(x => x.Title, opt => opt.Ignore()).
                   ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate)).
                    ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(x=>x.Url).ToList()));
        }
    }
}
