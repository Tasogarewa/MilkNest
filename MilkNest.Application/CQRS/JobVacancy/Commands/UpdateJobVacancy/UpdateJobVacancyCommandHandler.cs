using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.UpdateJobVacancy
{
    public class UpdateJobVacancyCommandHandler : IRequestHandler<UpdateJobVacancyCommand, Guid>
    {
        private readonly IJobVacancyService _jobvacancyService;

        public UpdateJobVacancyCommandHandler(IJobVacancyService jobvacancyService)
        {
            _jobvacancyService = jobvacancyService;
        }

        public async Task<Guid> Handle(UpdateJobVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _jobvacancyService.UpdateJobVacancyAsync(request);
        }
    }
}
