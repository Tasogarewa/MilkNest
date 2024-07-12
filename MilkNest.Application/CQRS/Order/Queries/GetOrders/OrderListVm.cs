using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Order.Queries.GetOrders
{
    public class OrderListVm
    {
        public List<OrderDto> OrderDtos { get; set; }
    }
}
