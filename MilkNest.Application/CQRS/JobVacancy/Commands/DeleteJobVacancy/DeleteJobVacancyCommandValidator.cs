using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.DeleteJobVacancy
{
    public class DeleteJobVacancyCommandValidator : AbstractValidator<DeleteJobVacancyCommand>
    {
        public DeleteJobVacancyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
