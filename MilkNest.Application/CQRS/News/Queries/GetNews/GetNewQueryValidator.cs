using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.News.Queries.GetNews
{
    public class GetNewQueryValidator : AbstractValidator<GetNewsQuery>
    {
        public GetNewQueryValidator()
        {
        }
    }
}
