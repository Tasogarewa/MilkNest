using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Queries.GetUsers
{
    public class UserDto:IMapWith<MilkNest.Domain.User>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOnline { get; set; }
        public  string? ImageUrl { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.User, UserDto>()
                .ForMember(x => x.IsAdmin, opt => opt.MapFrom(x => x.IsAdmin))
                .ForMember(x => x.IsOnline, opt => opt.MapFrom(x => x.IsOnline))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image.Url));
        }
    }

}
