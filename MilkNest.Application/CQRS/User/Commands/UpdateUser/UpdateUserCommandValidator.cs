using FluentValidation;
using MilkNest.Application.Common.ValidationBehaviors;


namespace MilkNest.Application.CQRS.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.UserName).NotNull().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.PasswordHash).ValidPassword();
            RuleFor(x => x.Id).NotNull().NotEmpty();
            
        }
    }
}
