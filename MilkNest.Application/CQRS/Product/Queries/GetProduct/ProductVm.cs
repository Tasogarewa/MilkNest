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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public List<string> Images { get; set; }
        public List<string> CommentContent { get; set; }
        public List<string> CommentUserName { get; set; }
        public List<string> CommentUserImage { get; set; }
        public List<Guid> CommentUserId { get; set; }
        public List<DateTime> CommentPublished { get; set; }
        public List<DateTime> CommentUpdated{ get; set; }
        public List<string> ReplyCommentContent { get; set; }
        public List<string> ReplyCommentUserName { get; set; }
        public List<string> ReplyCommentUserImage { get; set; }
        public List<Guid> ReplyCommentUserId { get; set; }
        public List<DateTime> ReplyCommentPublished { get; set; }
        public List<DateTime> ReplyCommentUpdated { get; set; }
        public List<int> OrderQuantity { get; set; }
        public List<string> OrderUserImg { get; set; }
        public List<string> OrderUserName { get; set; }
        public List<Guid> OrderUserGuid { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Product, ProductVm>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.Amount, opt => opt.MapFrom(x => x.Amount))
                 .ForMember(x => x.CommentContent, opt => opt.MapFrom(x => x.Comments.Select(x => x.Content).ToList()))
                .ForMember(x => x.CommentUserName, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.ApplicationUser.UserName).ToList()))
                 .ForMember(x => x.CommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.Image.Url).ToList()))
                .ForMember(x => x.CommentPublished, opt => opt.MapFrom(x => x.Comments.Select(x => x.DatePosted).ToList()))
                    .ForMember(x => x.CommentUserId, opt => opt.MapFrom(x => x.Comments.Select(x => x.UserId).ToList()))
                     .ForMember(x => x.CommentUpdated, opt => opt.MapFrom(x => x.Comments.Select(x => x.DateUpdated).ToList()))
                    .ForMember(x => x.ReplyCommentContent, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x=>x.Content).ToList())))
                .ForMember(x => x.ReplyCommentUserName, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x=>x.User.ApplicationUser.UserName)).ToList()))
                 .ForMember(x => x.ReplyCommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x=>x.User.Image.Url)).ToList()))
                .ForMember(x => x.ReplyCommentUserId, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x=>x.UserId)).ToList()))
                    .ForMember(x => x.ReplyCommentPublished, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x=>x.DatePosted)).ToList()))
                   .ForMember(x => x.ReplyCommentUpdated, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.DateUpdated)).ToList()))
                  .ForMember(x => x.OrderQuantity, opt => opt.MapFrom(x => x.Orders.Reverse().Take(8).Select(x => x.Quantity).ToList()))
                .ForMember(x => x.OrderUserImg, opt => opt.MapFrom(x => x.Orders.Reverse().Take(8).Select(x => x.User.Image.Url).ToList()))
                 .ForMember(x => x.OrderUserName, opt => opt.MapFrom(x => x.Orders.Reverse().Take(8).Select(x => x.User.ApplicationUser.UserName).ToList()))
                .ForMember(x => x.OrderUserGuid, opt => opt.MapFrom(x => x.Orders.Reverse().Take(8).Select(x => x.UserId).ToList()))
               .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(x=>x.Url).ToList()));

        }

    }
}
