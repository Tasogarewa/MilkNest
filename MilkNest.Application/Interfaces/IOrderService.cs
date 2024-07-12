using MediatR;
using MilkNest.Application.CQRS.Order.Commands.CreateOrder;
using MilkNest.Application.CQRS.Order.Commands.DeleteOrder;
using MilkNest.Application.CQRS.Order.Queries.GetOrders;
using MilkNest.Application.CQRS.Product.Commands.CreateProduct;
using MilkNest.Application.CQRS.Product.Commands.DeleteProduct;
using MilkNest.Application.CQRS.Product.Commands.UpdateProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<Guid> CreateOrderAsync(CreateOrderCommand createOrder);
        public Task<Unit> DeleteOrderAsync(DeleteOrderCommand deleteOrder);
        public Task<OrderListVm> GetOrdersAsync(GetOrdersQuery getOrders);
    }
}
