using System.Net;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Authentication;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v0/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthService _authService;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthService authService
        )
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(
            [FromBody] LoginUserDto loginUserDto
        )
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Incorrect object");

            JwtResponse jwtResponse = await _authService.LoginAsync(loginUserDto.Username, loginUserDto.Password);

            return Ok(jwtResponse);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(
            [FromBody] RegisterUserDto registerUserDto
        )
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Incorrect object");

            JwtResponse jwtResponse =
                await _authService.RegisterAsync(registerUserDto.Username, registerUserDto.Password);

            return Ok(jwtResponse);
        }
    }
}