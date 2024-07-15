using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Queries.GetProducts
{
    public class ProductDto:IMapWith<MilkNest.Domain.Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public List<string> Images { get; set; }
        public List<string> CommentContent { get; set; }
        public List<string> CommentUserName { get; set; }
        public List<string> CommentUserImage { get; set; }
        public List<DateTime> CommentPublished { get; set; }
        public int QuantityOrdered { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Product, ProductDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Amount))
                 .ForMember(x => x.CommentContent, opt => opt.MapFrom(x => x.Comments.Take(4).Select(x=>x.Content).ToList()))
                .ForMember(x => x.CommentUserName, opt => opt.MapFrom(x => x.Comments.Take(4).Select(x => x.User.UserName).ToList()))
                 .ForMember(x => x.CommentUserImage, opt => opt.MapFrom(x => x.Comments.Take(4).Select(x => x.User.Image.Url).ToList()))
                .ForMember(x => x.CommentPublished, opt => opt.MapFrom(x => x.Comments.Take(4).Select(x => x.DatePosted).ToList()))
                .ForMember(x => x.QuantityOrdered, opt => opt.MapFrom(x => x.Orders.Count))
               .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(x=>x.Url)));

        }
    }
}
