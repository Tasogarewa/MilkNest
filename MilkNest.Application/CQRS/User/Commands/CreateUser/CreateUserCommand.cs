using MediatR;
using Microsoft.AspNetCore.Http;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Commands.CreateUser
{
    public class CreateUserCommand:IRequest<Guid>
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile? Image { get; set; }
    }
}
