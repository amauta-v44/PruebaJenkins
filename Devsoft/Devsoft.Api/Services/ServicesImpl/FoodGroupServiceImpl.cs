using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.FoodGroup;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;

namespace Devsoft.Api.Services.ServicesImpl
{
	public class FoodGroupServiceImpl : IFoodGroupService
	{
		private readonly IFoodGroupRepository _foodGroupRepository;

		public FoodGroupServiceImpl(
			IFoodGroupRepository foodGroupRepository
		)
		{
			_foodGroupRepository = foodGroupRepository;
		}


		public async Task<FoodGroupResponseObject> FindOneByIdAsync(int id)
		{
			var item = await _foodGroupRepository.FindOneByIdAsync(id);

			if (item == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "FoodGroup not found");

			return FoodGroupResponseObject.FromEntity(item);
		}


		public async Task<FoodGroupResponseObject> FindOneByNameAsync(string name)
		{
			var item = await _foodGroupRepository.FindOneByNameAsync(name);

			if (item == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"FoodGroup with name {name} was not found");

			return FoodGroupResponseObject.FromEntity(item);
		}


		public async Task<IEnumerable<FoodGroupResponseObject>> FindAllAsync()
		{
			var items = await _foodGroupRepository.FindAllAsync();
			return items.Select(FoodGroupResponseObject.FromEntity);
		}


		public async Task<FoodGroupResponseObject> CreateOneAsync(FoodGroup t)
		{
			var item = await _foodGroupRepository.FindOneByNameAsync(t.Name);
			if (item != null)
				throw new HttpResponseException(HttpStatusCode.NotFound,
					$"FoodGroup with name {t.Name} already exists");

			await _foodGroupRepository.CreateOneAsync(t);
			return FoodGroupResponseObject.FromEntity(t);
		}


		public Task<FoodGroupResponseObject> UpdateOneAsync(int id, FoodGroup t)
		{
			throw new System.NotImplementedException();
		}


		public async Task<FoodGroupResponseObject> DeleteOneByIdAsync(int id)
		{
			var found = await _foodGroupRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, "FoodGroup not found");

			await _foodGroupRepository.DeleteOneAsync(found);
			return FoodGroupResponseObject.FromEntity(found);
		}


		public async Task DeleteAllAsync()
		{
			await _foodGroupRepository.DeleteAllAsync();
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