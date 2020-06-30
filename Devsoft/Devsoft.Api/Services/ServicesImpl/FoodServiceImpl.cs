using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Food;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Devsoft.Api.Services.ServicesImpl
{
	public class FoodServiceImpl : IFoodService
	{
		private readonly ILogger _logger;
		private readonly IFoodRepository _foodRepository;
		private readonly IFoodGroupRepository _foodGroupRepository;

		public FoodServiceImpl(
			ILogger<FoodServiceImpl> logger,
			IFoodRepository foodRepository,
			IFoodGroupRepository foodGroupRepository
		)
		{
			_logger = logger;
			_foodRepository = foodRepository;
			_foodGroupRepository = foodGroupRepository;
		}


		public async Task<FoodResponseObject> FindOneByIdAsync(int id)
		{
			var found = await _foodRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Food not found");

			return FoodResponseObject.FromEntity(found);
		}


		public async Task<FoodResponseObject> FindOneByNameAsync(string name)
		{
			var found = await _foodRepository.FindOneByNameAsync(name);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"Food with name {name} was not found");

			return FoodResponseObject.FromEntity(found);
		}


		public async Task<IEnumerable<FoodResponseObject>> FindAllAsync()
		{
			var food = await _foodRepository.FindAllAsync();
			var parsedItems = food.Select(FoodResponseObject.FromEntity);
			return parsedItems;
		}


		public async Task<FoodResponseObject> UpdateOneAsync(int id, Food t)
		{
			var found = await _foodRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Food not found");

			t.Id = id;
			await _foodRepository.UpdateOneAsync(t);

			return FoodResponseObject.FromEntity(found);
		}


		public async Task<FoodResponseObject> DeleteOneByIdAsync(int id)
		{
			var found = await _foodRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "Food not found");

			await _foodRepository.DeleteOneAsync(found);

			return FoodResponseObject.FromEntity(found);
		}


		public async Task DeleteAllAsync()
		{
			await _foodRepository.DeleteAllAsync();
		}


		public async Task<FoodResponseObject> CreateOneAsync(CreateFoodDto t)
		{
			var foodGroup = await _foodGroupRepository.FindOneByIdAsync(t.FoodGroupId);
			if (foodGroup == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "FoodGroup not found");

			var found = await _foodRepository.FindOneByNameAsync(t.Name);
			if (found != null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"Food with name {t.Name} already exists");

			var newFood = new Food
			{
				Name = t.Name,
				FoodGroupId = foodGroup.Id
			};

			await _foodRepository.CreateOneAsync(newFood);

			return FoodResponseObject.FromEntity(newFood);
		}
		
		
		// Validations
		public bool IsNameValid(string name)
		{
			if (string.IsNullOrEmpty(name)) return false;
			name = name.Trim();
			return 5 <= name.Length && name.Length <= 50;
		}
	}
}