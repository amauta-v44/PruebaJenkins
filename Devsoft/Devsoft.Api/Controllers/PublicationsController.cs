using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Publication;
using Devsoft.Api.Entities;
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
	public class PublicationsController : ControllerBase
	{
		private readonly ILogger<PublicationsController> _logger;
		private readonly IPublicationService _publicationService;

		public PublicationsController(
			ILogger<PublicationsController> logger,
			IPublicationService publicationService
		)
		{
			_logger = logger;
			_publicationService = publicationService;
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> FindAll()
		{
			var items = await _publicationService.FindAllAsync();
			// var parsedItems = items.Select(x => x.ToResponseObject());

			return Ok(items);
		}


		[HttpGet]
		[Route("/api/v0/users/{userId}/[controller]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> FindAllByUserIdAsync(
			[FromRoute] int userId
		)
		{
			var items = await _publicationService.FindAllByUserIdAsync(userId);
			// var parsedItems = items.Select(x => x.ToResponseObject());
			return Ok(items);
		}


		[HttpGet]
		[Route("/api/v0/users/me/[controller]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> FindAllByUserTokenAsync()
		{
			var userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var items = await _publicationService.FindAllByUserIdAsync(userId);
			// var parsedItems = items.Select(x => x());
			return Ok(items);
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> FindOneByIdAsync(
			[FromRoute] int id
		)
		{
			var item = await _publicationService.FindOneByIdAsync(id);
			return Ok(item);
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateOneAsync(
			[FromBody] CreatePublicationDto createPublicationDto
		)
		{
			var item = await _publicationService.CreateOneAsync(createPublicationDto);
			return Ok(item);
		}


		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateOneAsync(
			[FromRoute] int id,
			[FromBody] UpdatePublicationDto updatePublicationDto
		)
		{
			var item = await _publicationService.UpdateOneByIdAsync(id, updatePublicationDto);
			return Ok(item);
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteOneByIdAsync(
			[FromRoute] int id
		)
		{
			var item = await _publicationService.DeleteOneByIdAsync(id);
			return Ok(item);
		}
	}
}