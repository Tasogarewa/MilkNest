using MediatR;
using Microsoft.AspNetCore.Mvc;
using MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Commands.UpdateJobVacancy;
using MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies;
using MilkNest.Server.Controllers;
using System;
using System.Threading.Tasks;

namespace MilkNest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobVacancyController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateJobVacancy([FromForm] CreateJobVacancyCommand command)
        {
            var jobVacancyId = await Mediator.Send(command);
            return Ok(jobVacancyId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobVacancy(Guid id)
        {
            var command = new DeleteJobVacancyCommand
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobVacancies()
        {
            var query = new GetJobVacanciesQuery();
            var jobVacancies = await Mediator.Send(query);
            return Ok(jobVacancies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobVacancy(Guid id, [FromForm] UpdateJobVacancyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var updatedJobVacancyId = await Mediator.Send(command);
            if (updatedJobVacancyId == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}