using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Repositories
{
	public interface IFoodRepository
	{
		Task<IEnumerable<Food>> FindAllAsync();
		Task<Food> FindOneByIdAsync(int id);
		Task<Food> FindOneByNameAsync(string name);
		Task<Food> CreateOneAsync(Food food);
		Task<Food> UpdateOneAsync(Food food);
		Task<Food> DeleteOneAsync(Food food);
		Task DeleteAllAsync();
	}
}