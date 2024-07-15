using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
        {
            var userId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var query = new GetUserQuery { Id = id };
            var user = await Mediator.Send(query);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await Mediator.Send(query);
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedUserId = await Mediator.Send(command);
            if (updatedUserId == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}