using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Contracts.Requests;
using SimpleWebApi.Interfaces;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthController(
            IUserService userService, 
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequestModel request)
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            if (user != null)
            {
                return BadRequest($"User with this UserName already exist!");
            }

            await _userService.CreateNewUser(request.UserName, request.Password);

            return Ok($"User succesfully created!");
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequestModel request)
        {
            var checkCredentialsResult = await _userService.CheckCredentialsAsync(request.UserName, request.Password);
            if (!checkCredentialsResult)
            {
                return BadRequest($"Invalid credentials!");
            }

            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            var claimsIdentity = _userService.GetClaimsIdentityAsync(user);

            var token = _jwtService.GenerateToken(claimsIdentity);

            return Ok(token);
        }
    }
}
