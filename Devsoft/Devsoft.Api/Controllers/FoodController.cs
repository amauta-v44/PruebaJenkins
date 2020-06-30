using System.Threading.Tasks;
using Devsoft.Api.Dtos.Food;
using Devsoft.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Controllers
{
	[ApiController]
	[Route("api/v0/[controller]")]
	[Authorize]
	public class FoodController : ControllerBase
	{
		private readonly ILogger<FoodController> _logger;
		private readonly IFoodService _foodService;

		public FoodController(
			ILogger<FoodController> logger,
			IFoodService foodService
		)
		{
			_logger = logger;
			_foodService = foodService;
		}

		
		[HttpGet]
		public async Task<IActionResult> FindAllAsync()
		{
			var items = await _foodService.FindAllAsync();
			return Ok(items);
		}

		
		[HttpGet("{id}")]
		public async Task<IActionResult> FindOneById(
			[FromRoute] int id
		)
		{
			var item = await _foodService.FindOneByIdAsync(id);
			return Ok(item);
		}

		
		[HttpPost]
		public async Task<IActionResult> CreateOne(
			[FromBody] CreateFoodDto createFoodDto
		)
		{
			var item = await _foodService.CreateOneAsync(createFoodDto);
			return Ok(item);
		}

		
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOneById(
			[FromRoute] int id
		)
		{
			var item = await _foodService.DeleteOneByIdAsync(id);
			return Ok(item);
		}
	}
}