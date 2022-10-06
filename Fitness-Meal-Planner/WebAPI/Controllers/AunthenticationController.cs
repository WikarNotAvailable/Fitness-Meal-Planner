using Application.Dtos.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //Controller used for authentication of users
    [ApiController]
    [ApiVersion("1.0")]
    [Route("authentication")]
    public class AunthenticationController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly ITokenService tokenService;
        private readonly IConfiguration config;
        public AunthenticationController(IUsersService _usersService, ITokenService _tokenService, IConfiguration _config)
        {
            usersService = _usersService;
            tokenService = _tokenService;
            config = _config;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userLoginCredentials)
        {
            var user = await usersService.GetUserAsync(userLoginCredentials);
            if (user == null)
                return Unauthorized();
            else
            {
                var tokenString = tokenService.GenerateJWT(user, config);
                return Ok(new { token = tokenString });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userInfo)
        {
            if (userInfo.emailAddress == "" || userInfo.username == "" || userInfo.password == "")
                return BadRequest("You have to fill all fields!");

            var user = await usersService.GetUserByUsernameAsync(userInfo.username);
            if (user != null)
                return BadRequest("This username has been already taken!");

            return Ok(await usersService.AddUserAsync(userInfo));

        }
        [AllowAnonymous]
        [HttpPut("changePassword/{username}")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword)
        {
            if (username == null || newPassword == null)
                return BadRequest("You have to fill all fields!");

            var user = await usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            await usersService.ChangePasswordAsync(username, newPassword);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpPut("changeEmail/{username}")]
        public async Task<IActionResult> ChangeEmail(string username, string email)
        {
            if (username == null || email == null)
                return BadRequest("You have to fill all fields");

            var user = await usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            await usersService.ChangeEmailAsync(username, email);
            return NoContent();
        }
        [Authorize]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();
            else
                return Ok(user);
        }
    }
}
