using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy
{
    public class DeleteJobVacancyCommandHandle:IRequestHandler<DeleteJobVacancyCommand,Unit>
    {
        private readonly IJobVacancyService _jobvacancyService;

        public DeleteJobVacancyCommandHandle(IJobVacancyService jobvacancyService)
        {
            _jobvacancyService = jobvacancyService;
        }

        public async Task<Unit> Handle(DeleteJobVacancyCommand request, CancellationToken cancellationToken)
        {
            return await _jobvacancyService.DeleteJobVacancyAsync(request);
        }
    }
}
