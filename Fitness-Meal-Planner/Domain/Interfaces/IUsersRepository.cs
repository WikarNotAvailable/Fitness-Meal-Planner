using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    //interface used for implementation of users' repository
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(string _username, string _password);
        Task<User> GetUserByUsernameAsync(string _username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
