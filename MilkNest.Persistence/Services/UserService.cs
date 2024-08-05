using AutoMapper;
using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Application.CQRS.User.Commands.CreateUser;
using MilkNest.Application.CQRS.User.Commands.DeleteUser;
using MilkNest.Application.CQRS.User.Commands.UpdateUser;
using MilkNest.Application.CQRS.User.Queries.GetUser;
using MilkNest.Application.CQRS.User.Queries.GetUsers;
using MilkNest.Application.Interfaces;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MilkNest.Persistence.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public UserService(IRepository<User> userRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> CreateUserAsync(CreateUserCommand createUser)
        {

          var User = await _userRepository.CreateAsync(new User() { DateRegistered = DateTime.Now, Image = new Domain.Image() { Url = await _fileStorageService.SaveFileAsync(createUser.Image) } });
            return User.Id;
        }

        public async Task<Unit> DeleteUserAsync(DeleteUserCommand deleteUser)
        {
           var User = await _userRepository.GetAsync(deleteUser.Id);
            if (User != null)
            {
                await _fileStorageService.DeleteImageAsync(User.Image.Id);
              
                await _userRepository.DeleteAsync(deleteUser.Id);
                return Unit.Value;
            }
            else
            {
                NotFoundException.Throw(User, deleteUser.Id);
                return Unit.Value;
            }
        }

        public async Task<UserVm> GetUserAsync(GetUserQuery getUser)
        {
            var User = await _userRepository.GetAsync(getUser.Id);
            if (User != null)
            {
                return _mapper.Map<UserVm>(User);
            }
            else
            {
                NotFoundException.Throw(User, getUser.Id);
                return null;
            }
            
        }

        public async Task<UserListVm> GetUsersAsync(GetUsersQuery getUsers)
        {
            var Users = await _userRepository.GetAllAsync();
            if(Users!=null)
            {
                var userDtos = _mapper.ProjectTo<UserDto>(Users.AsQueryable()).ToList();
                return new UserListVm() { UserDtos = userDtos };
            }
            else
            {
                NotFoundException.ThrowRange(Users);
                return null;
            }
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserCommand updateUser)
        {
            var User = await _userRepository.GetAsync(updateUser.Id);
            if (User != null)
            {
                User.Image.Url = await _fileStorageService.UpdateFileAsync(updateUser.Image,User.Image.Url);
                User.ApplicationUser.UserName = updateUser.UserName;
                User.ApplicationUser.Email = updateUser.Email;
                User.ApplicationUser.PasswordHash = updateUser.PasswordHash;
                await _userRepository.UpdateAsync(User);
                return updateUser.Id;
            }
            else
            {
                NotFoundException.Throw(User, updateUser.Id);
                return Guid.Empty;
            }
        }
    }
}
