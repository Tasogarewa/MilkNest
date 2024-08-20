using FluentValidation;
using MilkNest.Application.Common.ValidationBehaviors;
using MilkNest.Application.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace MilkNest.Application.CQRS.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
          
        }
      
    }
}