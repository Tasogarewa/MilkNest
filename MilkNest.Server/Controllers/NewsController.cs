using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.News.Commands.CreateNews;
using MilkNest.Application.CQRS.News.Commands.DeleteNews;
using MilkNest.Application.CQRS.News.Commands.UpdateNews;
using MilkNest.Application.CQRS.News.Queries.GetNews;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsCommand command)
        {
            var newsId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetSingleNews), new { id = newsId }, newsId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var command = new DeleteNewsCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleNews(Guid id)
        {
            var query = new GetSingleNewsQuery { Id = id };
            var news = await Mediator.Send(query);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var query = new GetNewsQuery();
            var newsList = await Mediator.Send(query);
            return Ok(newsList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(Guid id, [FromForm] UpdateNewsCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedNewsId = await Mediator.Send(command);
            if (updatedNewsId == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}