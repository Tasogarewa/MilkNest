using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.Order.Commands.CreateOrder;
using MilkNest.Application.CQRS.Order.Commands.DeleteOrder;
using MilkNest.Application.CQRS.Order.Queries.GetOrders;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await Mediator.Send(command);
            return Ok(orderId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var query = new GetOrdersQuery();
            var orders = await Mediator.Send(query);
            return Ok(orders);
        }
    }
}