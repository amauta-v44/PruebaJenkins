using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Food;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Services
{
	public interface IFoodService
	{
		Task<IEnumerable<FoodResponseObject>> FindAllAsync();
		Task<FoodResponseObject> FindOneByIdAsync(int id);
		Task<FoodResponseObject> FindOneByNameAsync(string name);
		Task<FoodResponseObject> CreateOneAsync(CreateFoodDto t);
		Task<FoodResponseObject> DeleteOneByIdAsync(int id);
		Task DeleteAllAsync();
	}
}