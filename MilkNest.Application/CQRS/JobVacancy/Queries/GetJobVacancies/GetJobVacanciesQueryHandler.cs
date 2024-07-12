using MediatR;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies
{
    public class GetJobVacanciesQueryHandler : IRequestHandler<GetJobVacanciesQuery, JobVacancyListVm>
    {
        private readonly IJobVacancyService _jobvacancyService;

        public GetJobVacanciesQueryHandler(IJobVacancyService jobvacancyService)
        {
            _jobvacancyService = jobvacancyService;
        }

        public async Task<JobVacancyListVm> Handle(GetJobVacanciesQuery request, CancellationToken cancellationToken)
        {
            return await _jobvacancyService.GetJobVacanciesAsync(request);
        }
    }
}
