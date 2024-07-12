using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Commands.CreateComment
{
    public class CreateCommentCommand:IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}
