using Application.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    //interface for service working on users repository
    public interface IUsersService
    {
        Task<UserDto> GetUserAsync(LoginDto userCredentials);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> AddUserAsync(RegisterDto userDto);
        Task ChangePasswordAsync(string username, string password);
        Task ChangeEmailAsync (string username, string email);
        Task DeleteUserAsync (string username);
    }
}
