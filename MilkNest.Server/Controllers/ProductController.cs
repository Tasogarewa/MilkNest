using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.Product.Commands.CreateProduct;
using MilkNest.Application.CQRS.Product.Commands.DeleteProduct;
using MilkNest.Application.CQRS.Product.Commands.UpdateProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProduct;
using MilkNest.Application.CQRS.Product.Queries.GetProducts;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
        {
            var productId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var query = new GetProductQuery { Id = id };
            var product = await Mediator.Send(query);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var query = new GetProductsQuery();
            var products = await Mediator.Send(query);
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedProductId = await Mediator.Send(command);
            if (updatedProductId == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}