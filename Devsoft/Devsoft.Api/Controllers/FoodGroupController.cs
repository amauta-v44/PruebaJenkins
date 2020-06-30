using System.Linq;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.FoodGroup;
using Devsoft.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Controllers
{
    [ApiController]
    [Route("api/v0/[controller]")]
    [Authorize]
    public class FoodGroupController : ControllerBase
    {
        private readonly ILogger<FoodGroupController> _logger;
        private readonly IFoodGroupService _foodGroupService;


        public FoodGroupController(
            ILogger<FoodGroupController> logger,
            IFoodGroupService foodGroupService
        )
        {
            _logger = logger;
            _foodGroupService = foodGroupService;
        }


        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var items = await _foodGroupService.FindAllAsync();
            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> FindOneById(
            [FromRoute] int id
        )
        {
            var item = await _foodGroupService.FindOneByIdAsync(id);

            return Ok(item);
        }


        [HttpPost]
        public async Task<ActionResult> CreateOne
        (
            [FromBody] CreateFoodGroupDto createFoodGroupDto
        )
        {
            var item = await _foodGroupService.CreateOneAsync(createFoodGroupDto.ToEntity());
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOneById(
            [FromRoute] int id
        )
        {
            var item = await _foodGroupService.DeleteOneByIdAsync(id);
            return Ok(item);
        }
    }
}