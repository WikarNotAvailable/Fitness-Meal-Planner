using Application.Dtos.UserDtos;
using Application.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace Fitness_Meal_Planner_Testing.ControllersTests
{
    public class VerifiableUsersLogger : ILogger<AuthenticationController>
    {
        public int calledCount { get; set; }

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();
        public bool IsEnabled(LogLevel logLevel) => throw new NotImplementedException();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            calledCount++;
        }
    }

    public class AuthenticationControllerTests
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly VerifiableUsersLogger _logger;

        public AuthenticationControllerTests()
        {
            _usersService = A.Fake<IUsersService>();
            _tokenService = A.Fake<ITokenService>();   
            _config = A.Fake<IConfiguration>();
            _logger = new VerifiableUsersLogger();
        }

        [Fact]
        public async void AuthenticationController_Login_ReturnsOK()
        {
            //Arrange
            var user = A.Fake<UserDto>();
            var userLoginCredentials = A.Fake<LoginDto>();
            var tokenString = "exemplaryString";
            A.CallTo(() => _usersService.GetUserAsync(userLoginCredentials)).Returns(user);
            A.CallTo(() => _tokenService.GenerateJWT(user, _config)).Returns(tokenString);
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.Login(userLoginCredentials);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void AuthenticationController_Register_ReturnsCreated()
        {
            //Arrange
            UserDto? user = null;
            var newUser = A.Fake<UserDto>();
            var userInfo = A.Fake<RegisterDto>();
            A.CallTo(() => _usersService.GetUserByUsernameAsync(userInfo.Username)).Returns(user);
            A.CallTo(() => _usersService.AddUserAsync(userInfo)).Returns(newUser);
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.Register(userInfo);

            //Assert
            var result = actionResult.Result as CreatedResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void AuthenticationController_ChangePassword_ReturnsCreatedAtAction()
        {
            //Arrange
            var username = "username";
            var newPassword = "newPassword";
            var user = A.Fake<UserDto>();
            var newUser = A.Fake<UserDto>();
            A.CallTo(() => _usersService.GetUserByUsernameAsync(username)).Returns(user);
            A.CallTo(() => _usersService.ChangePasswordAsync(username, newPassword)).Returns(newUser);
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.ChangePassword(username, newPassword);

            //Assert
            var result = actionResult.Result as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedAtActionResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void AuthenticationController_ChangeEmail_ReturnsCreatedAtAction()
        {
            //Arrange
            var username = "username";
            var newEmail = "newEmail";
            var user = A.Fake<UserDto>();
            var newUser = A.Fake<UserDto>();
            A.CallTo(() => _usersService.GetUserByUsernameAsync(username)).Returns(user);
            A.CallTo(() => _usersService.ChangeEmailAsync(username, newEmail)).Returns(newUser);
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.ChangeEmail(username, newEmail);

            //Assert
            var result = actionResult.Result as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedAtActionResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void AuthenticationController_GetUser_ReturnsOK()
        {
            //Arrange
            var username = "username";
            var user = A.Fake<UserDto>();
            A.CallTo(() => _usersService.GetUserByUsernameAsync(username)).Returns(user);
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.GetUser(username);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            _logger.calledCount.Equals(1);
        }

        [Fact]
        public async void AuthenticationController_DeleteUser_ReturnsNoContent()
        {
            //Arrange
            var username = "username";
            var user = A.Fake<UserDto>();
            A.CallTo(() => _usersService.GetUserByUsernameAsync(username)).Returns(user);
            A.CallTo(() => _usersService.DeleteUserAsync(username)).DoesNothing();
            var controller = new AuthenticationController(_usersService, _tokenService, _config, _logger);

            //Act
            var actionResult = await controller.DeleteUser(username);

            //Assert
            var result = actionResult as NoContentResult;
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
            _logger.calledCount.Equals(1);
        }
    }
}
