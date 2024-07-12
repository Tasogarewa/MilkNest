using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Commands.CreateNews
{
    public class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        public CreateNewsCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull().MaximumLength(20000).MinimumLength(1000);
            RuleFor(x => x.Images).NotNull().NotEmpty();
            RuleFor(x=>x.Title).NotEmpty().NotNull().MaximumLength(500).MinimumLength(4);
        }
    }
}
