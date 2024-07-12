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
        public  MilkNest.Domain.User User { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public DateTime DateUpdated { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MilkNest.Domain.Comment, CommentDto>().
                ForMember(x => x.DatePosted, opt => opt.MapFrom(x => x.DatePosted)).
                ForMember(x => x.Content,opt => opt.MapFrom(x => x.Content)).
                ForMember(x => x.DateUpdated, opt => opt.MapFrom(x => x.DateUpdated)).
                ForMember(x => x.User, opt => opt.MapFrom(x => x.User));
        }
    }
}
