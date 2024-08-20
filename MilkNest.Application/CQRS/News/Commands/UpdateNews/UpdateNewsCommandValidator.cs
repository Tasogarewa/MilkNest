using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Commands.UpdateNews
{
    public class UpdateNewsCommandValidator:AbstractValidator<UpdateNewsCommand>
    {
        public UpdateNewsCommandValidator()
        {
            //RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(20000).MinimumLength(1000);
            //RuleFor(x => x.Images).NotNull().NotEmpty();
            //RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(500).MinimumLength(4);
        }
    }
}
