using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Order.Commands.DeleteOrder
{
    public class DeleteOrderCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
