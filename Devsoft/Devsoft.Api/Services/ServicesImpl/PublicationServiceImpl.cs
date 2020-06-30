using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Publication;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Services.ServicesImpl
{
	public class PublicationServiceImpl : IPublicationService
	{
		private readonly IPublicationsRepository _publicationsRepository;
		private readonly IUsersRepository _usersRepository;
		private readonly IFoodRepository _foodRepository;
		private readonly ILogger<PublicationServiceImpl> _logger;

		public PublicationServiceImpl(
			IPublicationsRepository publicationsRepository,
			IUsersRepository usersRepository,
			IFoodRepository foodRepository,
			ILogger<PublicationServiceImpl> logger
		)
		{
			_publicationsRepository = publicationsRepository;
			_usersRepository = usersRepository;
			_foodRepository = foodRepository;
			_logger = logger;
		}


		public async Task<PublicationResponseObject> FindOneByIdAsync(int id)
		{
			var found = await _publicationsRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Publication not found");

			return PublicationResponseObject.FromEntity(found);
		}


		public async Task<IEnumerable<PublicationResponseObject>> FindAllAsync()
		{
			var publications = await _publicationsRepository.FindAllAsync();
			var parsedItems = publications.Select(PublicationResponseObject.FromEntity);
			return parsedItems;
		}


		public async Task<PublicationResponseObject> UpdateOneByIdAsync(int id, UpdatePublicationDto t)
		{
			var found = await _publicationsRepository.FindOneByIdAsync(id);
			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id {id} was not found");

			// Validations
			if (!IsDescriptionValid(t.Description))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Description is not valid");

			if (!IsStockValid(t.Stock))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Stock is invalid");

			if (!IsPricePerUnitValid(t.PricePerUnit))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "PricePerUnit is invalid");

			// if (!AreDatesValid(t.StartDate, t.EndDate))
				// throw new HttpResponseException(HttpStatusCode.BadRequest, "Dates are invalid");

			if (!IsImageUrlValid(t.ImageUrl))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "ImageUrl is invalid");

			var updateEntity = t.ToEntity();
			updateEntity.Id = id;
			updateEntity.UserId = found.UserId;
			updateEntity.UpdatedAt = DateTime.Now;

			await _publicationsRepository.UpdateOneAsync(updateEntity);

			var entity = await _publicationsRepository.FindOneByIdAsync(id);

			return PublicationResponseObject.FromEntity(entity);
		}


		public async Task<PublicationResponseObject> DeleteOneByIdAsync(int id)
		{
			var found = await _publicationsRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Publication not found");

			await _publicationsRepository.DeleteOneAsync(found);

			return PublicationResponseObject.FromEntity(found);
		}


		public async Task DeleteAllAsync()
		{
			await _publicationsRepository.DeleteAllAsync();
		}


		public async Task<PublicationResponseObject> CreateOneAsync(CreatePublicationDto t)
		{
			// Validando que exista el usuario
			var foundUser = await _usersRepository.FindOneByIdAsync(t.UserId);
			if (foundUser == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "User not found");


			// Validando que exista Food
			var foundFood = await _foodRepository.FindOneByIdAsync(t.FoodId);
			if (foundFood == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Food not found");


			// Validando que exista la publicacion no exista
			var found = await _publicationsRepository.FindOneByUserIdAndFoodIdAndDescriptionAsync(t.UserId, t.FoodId, t.Description);
			if (found != null)
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Publication already exists");


			// Validaciones
			if (!IsDescriptionValid(t.Description))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Description is not valid");

			if (!IsStockValid(t.Stock))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Stock is invalid");

			if (!IsPricePerUnitValid(t.PricePerUnit))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "PricePerUnit is invalid");

			// if (!AreDatesValid(t.StartDate, t.EndDate))
				// throw new HttpResponseException(HttpStatusCode.BadRequest, "Dates are invalid");

			if (!IsImageUrlValid(t.ImageUrl))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "ImageUrl is invalid");


			// Creation
			var newPublication = new Publication()
			{
				Description = t.Description,
				Stock = t.Stock,
				StartDate = t.StartDate,
				// EndDate = t.EndDate,
				ImageUrl = t.ImageUrl,
				IsPublished = t.IsPublished,
				PricePerUnit = t.PricePerUnit,
				FoodId = foundFood.Id,
				UserId = foundUser.Id,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};

			await _publicationsRepository.CreateOneAsync(newPublication);

			var entity = await _publicationsRepository.FindOneByIdAsync(newPublication.Id);

			return PublicationResponseObject.FromEntity(entity);
		}


		public async Task<IEnumerable<PublicationResponseObject>> FindAllByUserIdAsync(int userId)
		{
			var publications = await _publicationsRepository.FindAllByUserIdAsync(userId);

			var parsedItems = publications.Select(x => new PublicationResponseObject
			{
				Id = x.Id,
				Description = x.Description,
				Stock = x.Stock,
				StartDate = x.StartDate,
				// EndDate = x.EndDate,
				ImageUrl = x.ImageUrl,
				IsPublished = x.IsPublished,
				PricePerUnit = x.PricePerUnit,
				FoodGroupId = x.Food.FoodGroupId,
				FoodId = x.FoodId,
			});

			return parsedItems;
		}


		// Validation functions
		public bool IsDescriptionValid(string description)
		{
			if (string.IsNullOrEmpty(description)) return false;
			description = description.Trim();
			return 10 <= description.Length && description.Length <= 300;
		}

		public bool IsStockValid(int stock)
		{
			return stock >= 0;
		}

		public bool IsPricePerUnitValid(float price)
		{
			var priceString = price.ToString();
			var index = priceString.IndexOf('.');

			if (index == -1) return price >= 0.0;

			var size = priceString.Length - 1 - index;
			if (size > 2) return false;

			return price >= 0.0;
		}

		public bool AreDatesValid(DateTime startTime, DateTime endTime)
		{
			if (startTime > endTime) return false;
			var timeDifference = endTime.Subtract(startTime);
			return timeDifference.Days >= 1;
		}

		public bool IsImageUrlValid(string imageUrl)
		{
			if (imageUrl.Length < 17 || imageUrl.Length > 2048) return false;

			var regChecker = new Regex(@"^https?:\/\/.+.(png|jpg)$");

			return regChecker.IsMatch(imageUrl);
		}
	}
}