using Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(LoginDto userCredentials);
        Task<UserDto> GetUserByUsernameAsync(string _username);
        Task<UserDto> AddUserAsync(RegisterDto userDto);
        Task ChangePasswordAsync(string _username, string _password);
        Task ChangeEmailAsync (string _username, string _email);
    }
}
