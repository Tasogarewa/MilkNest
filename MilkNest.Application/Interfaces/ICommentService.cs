using MediatR;
using MilkNest.Application.CQRS.Comment.Commands.CreateComment;
using MilkNest.Application.CQRS.Comment.Commands.DeleteComment;
using MilkNest.Application.CQRS.Comment.Commands.UpdateComment;
using MilkNest.Application.CQRS.Comment.Queries.GetComments;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface ICommentService
    {
        public Task<Guid> CreateCommentAsync(CreateCommentCommand createComment);
        public Task<Unit> DeleteCommentAsync(DeleteCommentCommand deleteComment);
        public Task<Guid> UpdateCommentAsync(UpdateCommentCommand updateComment);
        public Task<CommentListVm> GetCommentsAsync(GetCommentsQuery getComments);
    }
}
