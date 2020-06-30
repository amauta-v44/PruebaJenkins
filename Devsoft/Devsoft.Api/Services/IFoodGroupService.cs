using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.FoodGroup;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Services
{
	public interface IFoodGroupService
	{
		Task<IEnumerable<FoodGroupResponseObject>> FindAllAsync();
		Task<FoodGroupResponseObject> FindOneByIdAsync(int id);
		Task<FoodGroupResponseObject> CreateOneAsync(FoodGroup foodGroup);
		Task<FoodGroupResponseObject> DeleteOneByIdAsync(int id);
		Task DeleteAllAsync();
	}
}