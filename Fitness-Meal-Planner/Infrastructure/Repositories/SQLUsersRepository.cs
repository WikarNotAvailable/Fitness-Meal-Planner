using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    // class working on sql database, more specifically on users table
    public class SQLUsersRepository : IUsersRepository
    {
        private readonly FitnessPlannerContext context;
        public SQLUsersRepository(FitnessPlannerContext _context)
        {
            context = _context;
        }

        public async Task<User> GetUserAsync(string _username, string _password)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.username == _username && u.password == _password);
        }

        public async Task<User> GetUserByUsernameAsync(string _username)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.username == _username);
        }
        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
