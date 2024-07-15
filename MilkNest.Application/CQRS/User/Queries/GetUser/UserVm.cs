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
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsOnline { get; set; }
        public  string? ImageUrl { get; set; }
        public List<int> OrderQuantity { get; set; }
        public List<string> OrderUserImg { get; set; }
        public List<string> OrderUserName { get; set; }
        public List<Guid> OrderUserGuid { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> ProductImages { get; set; }
        public List<Guid> ProductId{ get; set; }
        public List<decimal> Price { get; set; }
        public List<string> CommentContent { get; set; }
        public List<string> CommentUserName { get; set; }
        public List<string> CommentUserImage { get; set; }
        public List<Guid> CommentUserId { get; set; }
        public List<DateTime> CommentPublished { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.User, UserVm>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.DateRegistered, opt => opt.MapFrom(x => x.DateRegistered))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image.Url))
                .ForMember(x => x.OrderQuantity, opt => opt.MapFrom(x => x.Orders.Select(x => x.Quantity).ToList()))
                .ForMember(x => x.OrderUserImg, opt => opt.MapFrom(x => x.Orders.Select(x => x.User.Image.Url).ToList()))
                 .ForMember(x => x.OrderUserName, opt => opt.MapFrom(x => x.Orders.Select(x => x.User.UserName).ToList()))
                .ForMember(x => x.OrderUserGuid, opt => opt.MapFrom(x => x.Orders.Select(x => x.UserId).ToList()))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Orders.Select(x => x.Product.Name).ToList()))
                .ForMember(x => x.ProductImages, opt => opt.MapFrom(x => x.Orders.Select(x => x.Product.Images.Select(x=>x.Url)).ToList()))
                 .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Orders.Select(x => x.Product.Id).ToList()))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Orders.Select(x => x.Product.Price).ToList()))
             .ForMember(x => x.IsOnline, opt => opt.MapFrom(x => x.IsOnline))
             .ForMember(x => x.IsAdmin, opt => opt.MapFrom(x => x.IsAdmin))
              .ForMember(x => x.CommentContent, opt => opt.MapFrom(x => x.Comments.Select(x => x.Content).ToList()))
                .ForMember(x => x.CommentUserName, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.UserName).ToList()))
                 .ForMember(x => x.CommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.Image.Url).ToList()))
                .ForMember(x => x.CommentPublished, opt => opt.MapFrom(x => x.Comments.Select(x => x.DatePosted).ToList()))
                    .ForMember(x => x.CommentUserId, opt => opt.MapFrom(x => x.Comments.Select(x => x.UserId).ToList()));
        }
    }
}
