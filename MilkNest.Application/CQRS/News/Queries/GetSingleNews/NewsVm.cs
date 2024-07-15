using AutoMapper;
using MilkNest.Application.Common.Mapping;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class NewsVm:IMapWith<MilkNest.Domain.News>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
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
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.News, NewsVm>().  
                 ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title)).
                  ForMember(x => x.Content, opt => opt.MapFrom(x => x.Content)).
                   ForMember(x => x.PublishDate, opt => opt.MapFrom(x => x.PublishDate)).
                      ForMember(x => x.UpdateDate, opt => opt.MapFrom(x => x.UpdateDate))
                        .ForMember(x => x.CommentContent, opt => opt.MapFrom(x => x.Comments.Select(x => x.Content).ToList()))
                .ForMember(x => x.CommentUserName, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.UserName).ToList()))
                 .ForMember(x => x.CommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(x => x.User.Image.Url).ToList()))
                .ForMember(x => x.CommentPublished, opt => opt.MapFrom(x => x.Comments.Select(x => x.DatePosted).ToList()))
                    .ForMember(x => x.CommentUserId, opt => opt.MapFrom(x => x.Comments.Select(x => x.UserId).ToList()))
                    .ForMember(x => x.ReplyCommentContent, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.Content).ToList())))
                .ForMember(x => x.ReplyCommentUserName, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.User.UserName)).ToList()))
                 .ForMember(x => x.ReplyCommentUserImage, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.User.Image.Url)).ToList()))
                .ForMember(x => x.ReplyCommentUserId, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.UserId)).ToList()))
                    .ForMember(x => x.ReplyCommentPublished, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.DatePosted)).ToList()))
                   .ForMember(x => x.ReplyCommentUpdated, opt => opt.MapFrom(x => x.Comments.Select(x => x.Replies.Select(x => x.DateUpdated)).ToList()))
                    .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(x => x.Url).ToList()));
        }
    }
}
