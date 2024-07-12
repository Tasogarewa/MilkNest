using AutoMapper;
using MilkNest.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Order.Queries.GetOrders
{
    public class OrderDto:IMapWith<MilkNest.Domain.Order>
    {
        public MilkNest.Domain.User User { get; set; }
        public MilkNest.Domain.Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Order, OrderDto>().
                ForMember(x => x.Quantity, opt => opt.MapFrom(x => x.Quantity)).
                ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product)).
                ForMember(x => x.OrderDate, opt => opt.MapFrom(x => x.OrderDate)).
                ForMember(x => x.User, opt => opt.MapFrom(x => x.User));
        }
    }
}
