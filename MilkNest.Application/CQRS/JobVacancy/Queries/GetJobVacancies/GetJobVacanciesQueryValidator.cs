using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Queries.GetJobVacancies
{
    public class GetJobVacanciesQueryValidator : AbstractValidator<GetJobVacanciesQuery>
    {
        public GetJobVacanciesQueryValidator()
        {
        }
    }
}
