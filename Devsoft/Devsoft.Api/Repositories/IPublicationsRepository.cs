using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Repositories
{
	public interface IPublicationsRepository
	{
		Task<ICollection<Publication>> FindAllAsync();
		Task<ICollection<Publication>> FindAllByUserIdAsync(int id);
		Task<Publication> FindOneByIdAsync(int id);
		Task<Publication> FindOneByUserIdAndFoodIdAndDescriptionAsync(int userId, int foodId, string description);
		Task<Publication> CreateOneAsync(Publication publication);
		Task<Publication> UpdateOneAsync(Publication publication);
		Task<Publication> DeleteOneAsync(Publication publication);
		Task DeleteAllAsync();
	}
}