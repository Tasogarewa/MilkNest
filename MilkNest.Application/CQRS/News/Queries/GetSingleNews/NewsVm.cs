using AutoMapper;
using MilkNest.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class NewsVm : IMapWith<MilkNest.Domain.News>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<string> Images { get; set; }
        public List<string> CommentContent { get; set; }
        public List<string> CommentUserName { get; set; }
        public List<string> CommentUserImage { get; set; }
        public List<Guid> CommentUserId { get; set; }
        public List<DateTime> CommentPublished { get; set; }
        public List<DateTime> CommentUpdated { get; set; }
        public List<string> ReplyCommentContent { get; set; }
        public List<string> ReplyCommentUserName { get; set; }
        public List<string> ReplyCommentUserImage { get; set; }
        public List<Guid> ReplyCommentUserId { get; set; }
        public List<DateTime> ReplyCommentPublished { get; set; }
        public List<DateTime> ReplyCommentUpdated { get; set; }
        public string Language { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.News, NewsVm>()
                .ForMember(x => x.Language, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.Ignore())
                .ForMember(x => x.Description, opt => opt.Ignore())
                .ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate))
                .ForMember(x => x.UpdateDate, opt => opt.MapFrom(x => x.UpdateDate))
                .ForMember(x => x.CommentContent, opt => opt.MapFrom(x => x.Comments.Select(c => c.Content).ToList()))
                .ForMember(x => x.CommentUserName, opt => opt.MapFrom(x => x.Comments.Select(c => c.User.ApplicationUser.UserName).ToList()))
                .ForMember(x => x.CommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(c => c.User.Image != null ? c.User.Image.Url : null).ToList()))
                .ForMember(x => x.CommentUserId, opt => opt.MapFrom(x => x.Comments.Select(c => c.UserId).ToList()))
                .ForMember(x => x.CommentPublished, opt => opt.MapFrom(x => x.Comments.Select(c => c.DatePosted).ToList()))
                .ForMember(x => x.CommentUpdated, opt => opt.MapFrom(x => x.Comments.Select(c => c.DateUpdated).ToList()))
                .ForMember(x => x.ReplyCommentContent, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.Content)).ToList()))
                .ForMember(x => x.ReplyCommentUserName, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.User.ApplicationUser.UserName)).ToList()))
                .ForMember(x => x.ReplyCommentUserImage, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.User.Image != null ? r.User.Image.Url : null)).ToList()))
                .ForMember(x => x.ReplyCommentUserId, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.UserId)).ToList()))
                .ForMember(x => x.ReplyCommentPublished, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.DatePosted)).ToList()))
                .ForMember(x => x.ReplyCommentUpdated, opt => opt.MapFrom(x => x.Comments.SelectMany(c => c.Replies.Select(r => r.DateUpdated)).ToList()))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(i => i.Url).ToList()));
              
        }
    }
}