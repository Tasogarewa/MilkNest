using MediatR;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface IUserService
    {
        public Task<Guid> CreateUserAsync(CreateUserCommand createUser);
        public Task<Unit> DeleteUserAsync(DeleteUserCommand deleteUser);
        public Task<UserVm> GetUserAsync(GetUserQuery getUser);
        public Task<Guid> UpdateUserAsync(UpdateUserCommand updateUser);
        public Task<UserListVm> GetUsersAsync(GetUsersQuery getUsers);
    }
}
