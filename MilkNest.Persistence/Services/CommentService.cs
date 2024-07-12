using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.Comment.Commands.CreateComment;
using MilkNest.Application.CQRS.Comment.Commands.DeleteComment;
using MilkNest.Application.CQRS.Comment.Commands.UpdateComment;
using MilkNest.Application.CQRS.Comment.Queries.GetComments;
using MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IRepository<User> userRepository, IRepository<Comment> commentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateCommentAsync(CreateCommentCommand createComment)
        {
            var Comment = await _commentRepository.CreateAsync(new Comment() {  Content = createComment.Content, User = await _userRepository.GetAsync(createComment.UserId),  DatePosted = DateTime.Now });
            return Comment.Id;
        }

        public async Task<Unit> DeleteCommentAsync(DeleteCommentCommand deleteComment)
        {
            var Comment = await _commentRepository.GetAsync(deleteComment.Id);
            if (Comment != null)
            {
                await _commentRepository.DeleteAsync(deleteComment.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(Comment, deleteComment.Id);
                return Unit.Value;
            }
        }

        public async Task<CommentListVm> GetCommentsAsync(GetCommentsQuery getComments)
        {

            var Comments = await _commentRepository.GetAllAsync();
            if (Comments != null)
            {
                var commentsDtos = _mapper.ProjectTo<CommentDto>(Comments.AsQueryable()).ToList();
                return new CommentListVm() {  CommentDtos = commentsDtos };
            }
            else
            {
                NotFoundException.ThrowRange(Comments);
                return null;
            }
        }

        public async Task<Guid> UpdateCommentAsync(UpdateCommentCommand updateComment)
        {
            var Comment = await _commentRepository.GetAsync(updateComment.Id);
          
            if (Comment != null)
            {

                Comment.DateUpdated = DateTime.Now;
                Comment.Content = updateComment.Content;
                await _commentRepository.UpdateAsync(Comment);
                return Comment.Id;
            }
            else
            {
                NotFoundException.Throw(Comment, updateComment.Id);
                return Guid.Empty;
            }
        }
    }
}
