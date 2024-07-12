using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MilkNest.Application.Common.ValidationBehaviors
{
 
    public static class PasswordValidation
    {
        public static bool ContainUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        public static bool ContainLowercase(string password)
        {
            return password.Any(char.IsLower);
        }

        public static bool ContainDigit(string password)
        {
            return password.Any(char.IsDigit);
        }

        public static bool ContainSpecialCharacter(string password)
        {
            return Regex.IsMatch(password, @"[\W_]");
        }
        public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull()
                .MinimumLength(8)
                .Must(PasswordValidation.ContainUppercase).WithMessage("Password must contain at least one uppercase letter.")
                .Must(PasswordValidation.ContainLowercase).WithMessage("Password must contain at least one lowercase letter.")
                .Must(PasswordValidation.ContainDigit).WithMessage("Password must contain at least one digit.")
                .Must(PasswordValidation.ContainSpecialCharacter).WithMessage("Password must contain at least one special character.");
        }
    }
}
