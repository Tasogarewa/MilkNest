using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Queries.GetComments
{
    public class CommentListVm
    {
        public List<CommentDto> CommentDtos { get; set; }
    }
}
