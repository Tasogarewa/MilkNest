using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;
        private readonly ITranslationService _translationService;

        public CommentService(IRepository<User> userRepository, IRepository<Comment> commentRepository, IRepository<Product> productRepository, IRepository<News> newsRepository, IMapper mapper, ITranslationService translationService)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _newsRepository = newsRepository;
            _mapper = mapper;
            _translationService = translationService;
        }

        public async Task<Guid> CreateCommentAsync(CreateCommentCommand createComment)
        {
            Comment parentComment = null;
            bool isReply = createComment.IsReply;

            if (isReply)
            {
                parentComment = await _commentRepository.GetAsync(Guid.Parse(createComment.ReplyCommentId.ToString()));

            }

            Comment newComment = new Comment
            {
                Content = createComment.Content,
                User = await _userRepository.GetAsync(createComment.UserId),
                DatePosted = DateTime.Now,
                Replies = null,
                ParentComment = parentComment
            };
           
            if (!isReply)
            {
                if (createComment.ProductId != null && createComment.ProductId != Guid.Empty)
                {
                    var product = await _productRepository.GetAsync(Guid.Parse(createComment.ProductId.ToString()));
                    product.Comments.Add(newComment);
                    await _productRepository.UpdateAsync(product);
                }
                else if (createComment.NewsId != null && createComment.NewsId != Guid.Empty)
                {
                    var news = await _newsRepository.GetAsync(Guid.Parse(createComment.NewsId.ToString()));
                    news.Comments.Add(newComment);
                    await _newsRepository.UpdateAsync(news);
                }
            }
            else
            {  
                if (createComment.ProductId != null && createComment.ProductId != Guid.Empty)
                {
                    var product = await _productRepository.GetAsync(Guid.Parse(createComment.ProductId.ToString()));
                    parentComment.Replies.Add(newComment);
                    product.Comments.Add(newComment);
                    
                    await _productRepository.UpdateAsync(product);
                }
                else if (createComment.NewsId != null && createComment.NewsId != Guid.Empty)
                {
                    var news = await _newsRepository.GetAsync(Guid.Parse(createComment.NewsId.ToString()));
                    parentComment.Replies.Add(newComment);
                    news.Comments.Add(newComment);
                    await _newsRepository.UpdateAsync(news);
                }
            }     
            return Guid.Parse(newComment.Id.ToString());
        }

        public async Task<Unit> DeleteCommentAsync(DeleteCommentCommand deleteComment)
        {
            var Comment = await _commentRepository.GetAsync(deleteComment.Id);
            if (Comment != null)
            {
                await RecursiveDeleteAsync(Comment);
                await _commentRepository.DeleteAsync(deleteComment.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(Comment, deleteComment.Id);
                return Unit.Value;
            }
        }
        private async Task RecursiveDeleteAsync(Comment comment)
        {
            foreach (var reply in comment.Replies.ToList())
            {
                await RecursiveDeleteAsync(reply);
            }

            await _commentRepository.DeleteAsync(comment.Id);
        }
        public async Task<CommentListVm> GetCommentsAsync(GetCommentsQuery getComments)
        {
            var comments = await _commentRepository.GetAllAsync();

            if (comments != null)
            {

                var commentDtos = _mapper.Map<List<CommentDto>>(comments);

                foreach (var commentDto in commentDtos)
                {

                    commentDto.Language = await _translationService.GetCurrentLanguageAsync();


                    var comment = comments.FirstOrDefault(c => c.Id == commentDto.Id);
                    if (comment != null)
                    {
                        var newsLocalization = comment.News?.Localizations.FirstOrDefault(l => l.Language == commentDto.Language);
                        if (newsLocalization != null)
                        {
                            commentDto.NewsTitle = newsLocalization.Title;
                        }

                        var productLocalization = comment.Product?.Localizations.FirstOrDefault(l => l.Language == commentDto.Language);
                        if (productLocalization != null)
                        {
                            commentDto.ProductTitle = productLocalization.Title;
                        }
                    }
                }

                return new CommentListVm() { CommentDtos = commentDtos };
            }
            else
            {
                NotFoundException.ThrowRange(comments);
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
