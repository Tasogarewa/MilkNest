using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
