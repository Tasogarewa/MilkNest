using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Queries.GetProduct
{
    public class ProductVm:IMapWith<MilkNest.Domain.Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public List<MilkNest.Domain.Image> Images { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Product, ProductVm>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Amount))
               .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images));

        }

    }
}
