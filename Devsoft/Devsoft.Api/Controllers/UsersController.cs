using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Users;
using Devsoft.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Controllers
{
    [ApiController]
    [Route("api/v0/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersService usersService
        )
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var items = await _usersService.FindAllAsync();
            var parsedItems = items.Select(x => x.ToResponseObject());
            return Ok(parsedItems);
        }

        [HttpGet("me")]
        public async Task<ActionResult> FindMe()
        {
            var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = await _usersService.FindOneByIdAsync(userId);

            return Ok(user.ToResponseObject());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> FindOneById(
            [FromRoute] int id
        )
        {
            var user = await _usersService.FindOneByIdAsync(id);
            return Ok(user.ToResponseObject());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateOne(
            [FromBody] CreateUserDto newUser
        )
        {
            var item = await _usersService.CreateOneAsync(newUser.ToEntity());

            return Ok(item.ToResponseObject());
        }
    }
}