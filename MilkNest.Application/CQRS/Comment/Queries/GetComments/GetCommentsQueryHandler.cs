using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Queries.GetComments
{
    public class GetCommentsQueryHandler:IRequestHandler<GetCommentsQuery,CommentListVm>
    {
        private readonly ICommentService _commentService;

        public GetCommentsQueryHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<CommentListVm> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            return await _commentService.GetCommentsAsync(request);
        }
    }
}
