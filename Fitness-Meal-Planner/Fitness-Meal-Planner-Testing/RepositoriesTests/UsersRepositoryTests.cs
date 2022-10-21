using Application.Mappings;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Fitness_Meal_Planner_Testing.RepositoriesTests
{
    public class UsersRepositoryTests
    {
        private async Task<FitnessPlannerContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FitnessPlannerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new FitnessPlannerContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Users.CountAsync() <= 0)
            {
                databaseContext.Users.Add(
                new User()
                {
                    Username = "Name1",
                    Password = "Password1",
                    EmailAddress = "Email1@gmail.com",
                    Role = "Guest"
                });
                databaseContext.Users.Add(
                new User()
                {
                    Username = "Name2",
                    Password = "Password2",
                    EmailAddress = "Email2@gmail.com",
                    Role = "Guest"
                });
                databaseContext.Users.Add(
                new User()
                {
                    Username = "Name3",
                    Password = "Password3",
                    EmailAddress = "Email3@gmail.com",
                    Role = "Guest"
                });
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async void UsersRepository_GetLoggingUserAsync_ReturnsUser()
        {
            //Arrange
            var username = "Name1";
            var password = "Password1";
            var dbContext = await GetDatabaseContext();
            var repository = new SQLUsersRepository(dbContext);

            //Act
            var user = await repository.GetLoggingUserAsync(username, password);

            //Assert
            user.Should().NotBeNull();
            user.Username.Equals("Name1");
        }

        [Fact]
        public async void UsersRepository_GetUserByUsernameAsync_ReturnsUser()
        {
            //Arrange
            var username = "Name1";
            var dbContext = await GetDatabaseContext();
            var repository = new SQLUsersRepository(dbContext);

            //Act
            var user = await repository.GetUserByUsernameAsync(username);

            //Assert
            user.Should().NotBeNull();
            user.Username.Equals("Name1");
        }

        [Fact]
        public async void UsersRepository_AddUserAsync_AddsUser()
        {
            //Arrange
            var newUser = new User
            {
                Username = "Name4",
                Password = "Password4",
                EmailAddress = "Email4@gmail.com",
                Role = "Guest"
            };
            var dbContext = await GetDatabaseContext();
            var repository = new SQLUsersRepository(dbContext);

            //Act
            await repository.AddUserAsync(newUser);

            //Assert
            var user = await repository.GetUserByUsernameAsync("Name4");
            user.Should().NotBeNull();
        }

        [Fact]
        public async void UsersRepository_UpdateUserAsync_UpdatesUser()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new SQLUsersRepository(dbContext);
            var mapper = AutoMapperConfig.Initialize();

            var newUser = new User
            {
                Username = "Name1",
                Password = "Password4",
                EmailAddress = "Email4@gmail.com",
                Role = "Guest"
            };
            var oldUser = await repository.GetUserByUsernameAsync("Name1");
            var user = mapper.Map(newUser, oldUser);

            //Act
            await repository.UpdateUserAsync(user);

            //Assert
            var userCheck = await repository.GetUserByUsernameAsync("Name1");
            userCheck.Should().NotBeNull();
            userCheck.Password.Equals("Password4");
            userCheck.EmailAddress.Equals("Email4@gmail.com");
        }

        [Fact]
        public async void UsersRepository_DeleteUserAsync_DeletesUser()
        {
            //Arrange   
            var dbContext = await GetDatabaseContext();
            var repository = new SQLUsersRepository(dbContext);
            var user = await repository.GetUserByUsernameAsync("Name1");

            //Act
            await repository.DeleteUserAsync(user);

            //Assert
            var userCheck = await repository.GetUserByUsernameAsync("Name1");
            userCheck.Should().BeNull();
        }
    }
}
