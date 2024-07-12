using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.CQRS.User.Queries.GetUsers
{
    public class UserListVm
    {
        public List<UserDto> UserDtos { get; set; }
    }
}
