using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Queries.GetUser
{
    public class GetUserQuery:IRequest<UserVm>
    {
        public Guid Id { get; set; }
    }
}
