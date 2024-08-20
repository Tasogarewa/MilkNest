using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.JobVacancy.Commands.CreateJobVacancy
{
    public class CreateJobVacancyCommandValidator : AbstractValidator<CreateJobVacancyCommand>
    {
        public CreateJobVacancyCommandValidator()
        {
            //RuleFor(x => x.Images).NotEmpty().NotNull();
            //RuleFor(x=>x.Title).NotEmpty().NotNull().MaximumLength(200).MinimumLength(4);
            //RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(5000).MinimumLength(100);
        }
    }
}
