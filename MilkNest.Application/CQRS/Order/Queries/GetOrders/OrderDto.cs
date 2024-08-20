using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Order.Queries.GetOrders
{
    public class OrderDto:IMapWith<MilkNest.Domain.Order>
    {
      
        public OrderDto()
        {
        }

     

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public Guid ProductId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    public string Language { get; set; }
        public async void Mapping(Profile profile)
        {
        
            profile.CreateMap<MilkNest.Domain.Order, OrderDto>().
                ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity)).
                ForMember(x => x.ProductName, opt => opt.Ignore()).
                   ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Product.Id)).
                      ForMember(x => x.ProductImage, opt => opt.MapFrom(x => x.Product.Images.First().Url)).
                        ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.ApplicationUser.UserName)).
                          ForMember(x => x.UserImage, opt => opt.MapFrom(x => x.User.Image.Url)).
                            ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id)).
                              ForMember(x => x.OrderDate, opt => opt.MapFrom(x => x.OrderDate));
        }
    }
}
