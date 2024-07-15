using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.Comment.Commands.CreateComment;
using MilkNest.Application.CQRS.Comment.Commands.DeleteComment;
using MilkNest.Application.CQRS.Comment.Commands.UpdateComment;
using MilkNest.Application.CQRS.Comment.Queries.GetComments;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command)
        {
            var commentId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetComments), new { id = commentId }, commentId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var command = new DeleteCommentCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var query = new GetCommentsQuery();
            var comments = await Mediator.Send(query);
            return Ok(comments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromForm] UpdateCommentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedCommentId = await Mediator.Send(command);
            if (updatedCommentId == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}