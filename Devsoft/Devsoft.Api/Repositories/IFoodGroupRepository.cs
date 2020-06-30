using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.FoodGroup;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Repositories
{
	public interface IFoodGroupRepository
	{
		Task<ICollection<FoodGroup>> FindAllAsync();
		Task<FoodGroup> FindOneByIdAsync(int id);
		Task<FoodGroup> FindOneByNameAsync(string name);
		Task<FoodGroup> CreateOneAsync(FoodGroup foodGroup);
		Task<FoodGroup> DeleteOneAsync(FoodGroup foodGroup);
		Task DeleteAllAsync();
	}
}