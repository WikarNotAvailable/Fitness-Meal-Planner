using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;
        public UsersService(IUsersRepository repository, IMapper mapper, IValidator<User> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<UserDto> GetUserAsync(LoginDto userCredentials)
        {
            var user = await _repository.GetUserAsync(userCredentials.Username, userCredentials.Password);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> AddUserAsync(RegisterDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);

            ValidationResult result = await _validator.ValidateAsync(newUser);
            if (!result.IsValid)
                return null;

            await _repository.AddUserAsync(newUser);
            return _mapper.Map<UserDto>(newUser);
        }
        public async Task<UserDto> ChangePasswordAsync(string _username, string _password)
        {
            var user = await _repository.GetUserByUsernameAsync(_username);
            User newUserInfo = new User
            {
                Username = _username,
                Password = _password,
                EmailAddress = user.EmailAddress,
                Role = user.Role
            };
            var newUser = _mapper.Map(newUserInfo, user);

            ValidationResult result = await _validator.ValidateAsync(newUser);
            if (!result.IsValid)
                return null;

            await _repository.UpdateUserAsync(newUser);
            return _mapper.Map<UserDto>(newUser);
        }
        public async Task<UserDto> ChangeEmailAsync(string _username, string _email)
        {
            var user = await _repository.GetUserByUsernameAsync(_username);
            User newUserInfo = new User
            {
                Username = _username,
                Password = user.Password,
                EmailAddress = _email,
                Role = user.Role
            };
            var newUser = _mapper.Map(newUserInfo, user);

            ValidationResult result = await _validator.ValidateAsync(newUser);
            if (!result.IsValid)
                return null;

            await _repository.UpdateUserAsync(newUser);
            return _mapper.Map<UserDto>(newUser);
        }
        public async Task DeleteUserAsync(string _username)
        {
            var user = await _repository.GetUserByUsernameAsync(_username);
            await _repository.DeleteUserAsync(user);
        }
    }
}
