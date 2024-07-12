using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Order.Queries.GetOrders
{
    public class GetOrdersQuery:IRequest<OrderListVm>
    {
    }
}
