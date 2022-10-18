using Application.Dtos.UserDtos;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Wrappers;

namespace WebAPI.Controllers
{
    //Controller used for authentication of users
    [ApiController]
    [ApiVersion("1.0")]
    [Route("auth")]
    public class AunthenticationController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly ILogger<AunthenticationController> _logger;
        public AunthenticationController(IUsersService usersService, ITokenService tokenService, IConfiguration config, ILogger<AunthenticationController> logger)
        {
            _usersService = usersService;
            _tokenService = tokenService;
            _config = config;
            _logger = logger;
        }
        [AllowAnonymous]
        [ResponseCache]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userLoginCredentials)
        {
            _logger.LogInformation("Login attempt.");

            var user = await _usersService.GetUserAsync(userLoginCredentials);
            if (user == null)
            {
                _logger.LogError("Login failed.");
                throw new IncorrectCredentialsException("Incorrect username or password.");
            }
            else
            {
                _logger.LogInformation("Login succeeded");
                var tokenString = _tokenService.GenerateJWT(user, _config);
                return Ok(new { token = tokenString });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userInfo)
        {

            _logger.LogInformation("Register attempt.");

            var user = await _usersService.GetUserByUsernameAsync(userInfo.Username);
            if (user != null)
            {
                _logger.LogError("Register failed, username has been already taken.");
                throw new IncorrectCredentialsException("Username has been already taken.");
            }

            var newUser = await _usersService.AddUserAsync(userInfo);

            if (newUser == null)
            {
                _logger.LogError("Register failed, incorrect credentials.");
                throw new IncorrectCredentialsException("Fields were not filled properly.");
            }
            return Created($"/register.{newUser.Username}", new Response<UserDto>(newUser));
        }
        [AllowAnonymous]
        [HttpPut("changePassword/{username}")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword)
        {
            _logger.LogInformation($"Changing password of {username}.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("Changing password failed, username has not been found.");
                throw new IncorrectCredentialsException("Username has not been found in the database.");
            }

            var newUser = await _usersService.ChangePasswordAsync(username, newPassword);

            if (newUser == null)
            {
                _logger.LogError("Changing password failed, incorrect password format.");
                throw new EntityValidatonException("Incorrect password format.");
            }
            return CreatedAtAction($"changePassword", new Response<UserDto>(newUser));
        }
        [AllowAnonymous]
        [HttpPut("changeEmail/{username}")]
        public async Task<IActionResult> ChangeEmail(string username, string email)
        {
            _logger.LogInformation($"Changing email of {username}.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("Changing password failed, username has not been found.");
                throw new IncorrectCredentialsException("Username has not been found in the database.");
            }

            var newUser = await _usersService.ChangeEmailAsync(username, email);

            if (newUser == null)
            {
                _logger.LogError("Changing email failed, incorrect email format.");
                throw new EntityValidatonException("Incorrect email format.");
            }
            return CreatedAtAction($"changeEmail", new Response<UserDto>(newUser));
        }
        [Authorize]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "username" })]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            _logger.LogInformation($"Getting user by username.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("The user was not found in the database.");
                throw new EntityNotFoundException("The passed username is wrong - the usern doesnt't exist.");
            }

            return Ok(new Response<UserDto>(user));
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            _logger.LogInformation($"Deleting user by username.");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                _logger.LogError("Deleting user failed, user was not found.");
                throw new EntityNotFoundException("The passed username is wrong - the usern doesnt't exist.");
            }

            await _usersService.DeleteUserAsync(username);
            return NoContent();
        }
    }
}
