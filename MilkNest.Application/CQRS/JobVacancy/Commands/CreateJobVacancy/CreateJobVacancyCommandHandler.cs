using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy
{
    public class CreateJobVacancyCommandHandler : IRequestHandler<CreateJobVacancyCommand, Guid>
    {
        private readonly IJobVacancyService _jobvacancyService;

        public CreateJobVacancyCommandHandler(IJobVacancyService jobvacancyService)
        {
            _jobvacancyService = jobvacancyService;
        }

        public async Task<Guid> Handle(CreateJobVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _jobvacancyService.CreateJobVacancyAsync(request);
        }
    }
}
