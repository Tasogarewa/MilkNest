using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Comment.Commands.CreateComment
{
    public class CreateCommentCommandValidator:AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Content).MaximumLength(5000).NotNull().NotEmpty();
        }
    }
}
