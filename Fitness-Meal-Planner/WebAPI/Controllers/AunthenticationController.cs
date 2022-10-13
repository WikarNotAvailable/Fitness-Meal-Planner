using Application.Dtos.UserDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ILogger<AunthenticationController> _loger;
        public AunthenticationController(IUsersService usersService, ITokenService tokenService, IConfiguration config, ILogger<AunthenticationController> loger)
        {
            _usersService = usersService;
            _tokenService = tokenService;
            _config = config;
            _loger = loger;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userLoginCredentials)
        {
            var user = await _usersService.GetUserAsync(userLoginCredentials);
            if (user == null)
                return Unauthorized();
            else
            {
                var tokenString = _tokenService.GenerateJWT(user, _config);
                return Ok(new { token = tokenString });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userInfo)
        {
            if (userInfo.EmailAddress == "" || userInfo.Username == "" || userInfo.Password == "")
                return BadRequest("You have to fill all fields!");

            var user = await _usersService.GetUserByUsernameAsync(userInfo.Username);
            if (user != null)
                return BadRequest("This username has been already taken!");

            return Ok(await _usersService.AddUserAsync(userInfo));
        }
        [AllowAnonymous]
        [HttpPut("changePassword/{username}")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword)
        {
            if (username == null || newPassword == null)
                return BadRequest("You have to fill all fields!");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            await _usersService.ChangePasswordAsync(username, newPassword);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpPut("changeEmail/{username}")]
        public async Task<IActionResult> ChangeEmail(string username, string email)
        {
            if (username == null || email == null)
                return BadRequest("You have to fill all fields");

            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            await _usersService.ChangeEmailAsync(username, email);
            return NoContent();
        }
        [Authorize]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound();

            await _usersService.DeleteUserAsync(username);
            return NoContent();
        }
    }
}
