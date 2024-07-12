using MediatR;
using Microsoft.AspNetCore.Http;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Commands.UpdateUser
{
    public class UpdateUserCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public IFormFile Image { get; set; }
    }
}
