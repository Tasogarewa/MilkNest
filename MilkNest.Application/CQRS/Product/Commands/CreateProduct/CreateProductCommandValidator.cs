using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.Amount).NotNull().NotEmpty().LessThan(500).WithMessage("There are so many products in stock every day");
            RuleFor(x=>x.Price).NotNull().NotEmpty().LessThan(10000).GreaterThan(100);
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.Images).NotNull().NotEmpty();
            RuleFor(x => x.Description).MinimumLength(80).MaximumLength(2000);
        }
    }
}
