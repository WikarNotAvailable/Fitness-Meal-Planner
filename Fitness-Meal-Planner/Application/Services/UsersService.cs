using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository repository;
        private readonly IMapper mapper;
        public UsersService(IUsersRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }
        
        public async Task<UserDto> GetUserAsync(LoginDto userCredentials)
        {
            var user = await repository.GetUserAsync(userCredentials.username, userCredentials.password);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string _username)
        {
            var user = await repository.GetUserByUsernameAsync(_username);
            return mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> AddUserAsync(RegisterDto userDto)
        {
            var newUser = mapper.Map<User>(userDto);
            await repository.AddUserAsync(newUser);
            return mapper.Map<UserDto>(newUser);
        }
        public async Task ChangePasswordAsync(string _username, string _password)
        {
            var user = await repository.GetUserByUsernameAsync(_username);
            User newUserInfo = new User
            {
                username = _username,
                password = _password,
                emailAddress = user.emailAddress,
                role = user.role
            };
            var newUser = mapper.Map(newUserInfo, user);
            await repository.UpdateUserAsync(newUser);
        }

        public async Task ChangeEmailAsync(string _username, string _email)
        {
            var user = await repository.GetUserByUsernameAsync(_username);
            User newUserInfo = new User
            {
                username = _username,
                password = user.password,
                emailAddress = _email,
                role = user.role
            };
            var newUser = mapper.Map(newUserInfo, user);
            await repository.UpdateUserAsync(newUser);
        }
    }
}
