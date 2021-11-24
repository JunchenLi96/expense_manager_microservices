using System.Threading.Tasks;
using Users.api.DomainModels;
using Users.api.RequestModels;
using Users.api.Services;
using Users.api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Users.api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest RegisterRequest)
        {
            if (await _userService.CheckUserExists(RegisterRequest.UserName)) return BadRequest("Username is taken");

            var user = new AppUser
            {
                UserName = RegisterRequest.UserName,
                PasswordHash = HashUtils.ComputeHash(RegisterRequest.Password),
            };
            await _userService.AddNewUserAsync(user);

            return Ok(new
            {
                Token = _tokenService.CreateToken(RegisterRequest.UserName)
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userService.GetUser(loginRequest.UserName);

            if (user == null) return BadRequest("Username and password doesn't match");

            string computedPassword = HashUtils.ComputeHash(loginRequest.Password);

            if (computedPassword != user.PasswordHash) return BadRequest("Username and password doesn't match");

            return Ok(new
            {
                Token = _tokenService.CreateToken(user.ID.ToString())
            });
        }
    }
}