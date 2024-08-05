using AutoMapper;
using MilkNest.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Queries.GetComments
{
    public class CommentDto:IMapWith<MilkNest.Domain.Comment>
    {
        public Guid Id { get; set; }
        public string UserImg { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<string>? Replies { get; set; }
        public List<DateTime>?  ReplyPosted { get; set; }
        public List<DateTime>? ReplyUpdated { get; set; }
        public List<string>? ReplyUserImg { get; set; }
        public List<string>? ReplyUserName { get; set; }
        public List<Guid>? ReplyUserId { get; set; }
        public string? ParentCommentUserImg { get; set; }
        public string? ParentCommentUserName { get; set; }
        public string? ParentCommentContent { get; set; }
        public Guid? ParentCommentUserId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string? NewsImg { get; set; }
        public string? NewsTitle { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImg { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Comment, CommentDto>()
                .ForMember(x => x.DatePosted, opt => opt.MapFrom(x => x.DatePosted))
                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.DateUpdated, opt => opt.MapFrom(x => x.DateUpdated))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.ApplicationUser.UserName))
                .ForMember(x => x.UserImg, opt => opt.MapFrom(x => x.User.Image != null ? x.User.Image.Url : null))
                .ForMember(x => x.Replies, opt => opt.MapFrom(x => x.Replies.Select(r => r.Content).ToList()))
                .ForMember(x => x.ReplyPosted, opt => opt.MapFrom(x => x.Replies.Select(r => r.DatePosted).ToList()))
                .ForMember(x => x.ReplyUpdated, opt => opt.MapFrom(x => x.Replies.Select(r => r.DateUpdated).ToList()))
                .ForMember(x => x.ReplyUserName, opt => opt.MapFrom(x => x.Replies.Select(r => r.User.ApplicationUser.UserName).ToList()))
                .ForMember(x => x.ReplyUserId, opt => opt.MapFrom(x => x.Replies.Select(r => r.User.Id).ToList()))
                .ForMember(x => x.ReplyUserImg, opt => opt.MapFrom(x => x.Replies.Select(r => r.User.Image != null ? r.User.Image.Url : null).ToList()))
                .ForMember(x => x.ParentCommentContent, opt => opt.MapFrom(src => src.ParentComment != null ? src.ParentComment.Content : null))
                .ForMember(x => x.ParentCommentUserName, opt => opt.MapFrom(src => src.ParentComment != null ? src.ParentComment.User.ApplicationUser.UserName : null))
                .ForMember(x => x.ParentCommentUserImg, opt => opt.MapFrom(src => src.ParentComment != null ? src.ParentComment.User.Image != null ? src.ParentComment.User.Image.Url : null : null))
                .ForMember(x => x.ParentCommentUserId, opt => opt.MapFrom(src => src.ParentComment != null ? src.ParentComment.UserId : (Guid?)null))
                .ForMember(x => x.NewsImg, opt => opt.MapFrom(src => src.News != null && src.News.Images.Any() ? src.News.Images.First().Url : null))
                .ForMember(x => x.NewsTitle, opt => opt.MapFrom(src => src.News != null ? src.News.Title : null))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(x => x.ParentCommentId, opt => opt.MapFrom(src => src.ParentCommentId != null ? src.ParentCommentId : null))
                .ForMember(x => x.ProductImg, opt => opt.MapFrom(src => src.Product != null && src.Product.Images.Any() ? src.Product.Images.First().Url : null));
        }
    }
}
