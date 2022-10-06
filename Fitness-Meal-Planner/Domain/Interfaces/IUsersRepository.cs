using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string _username, string _password);
        Task<User> GetUserByUsernameAsync(string _username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
