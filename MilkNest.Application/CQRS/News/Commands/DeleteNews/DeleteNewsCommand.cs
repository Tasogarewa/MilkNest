using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Commands.DeleteNews
{
    public class DeleteNewsCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
