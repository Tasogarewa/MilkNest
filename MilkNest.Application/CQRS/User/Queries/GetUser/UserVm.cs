using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Queries.GetUser
{
    public class UserVm:IMapWith<MilkNest.Domain.User>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsOnline { get; set; }
        public  Image? Image { get; set; }
        public  List<MilkNest.Domain.Order>? Orders { get; set; } 
        public  List<MilkNest.Domain.Comment>? Comments { get; set; } 
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.User, UserVm>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateRegistered, opt => opt.MapFrom(x => x.DateRegistered))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image))
               .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))
            .ForMember(x => x.Orders, opt => opt.MapFrom(x => x.Orders))
             .ForMember(x => x.IsOnline, opt => opt.MapFrom(x => x.IsOnline))
             .ForMember(x => x.IsAdmin, opt => opt.MapFrom(x => x.IsAdmin));
        }
    }
}
