using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Repositories
{
	public interface IUsersRepository
	{
		Task<ICollection<User>> FindAllAsync();
		Task<User> FindOneByIdAsync(int id);
		Task<User> FindOneByUsernameAsync(string username);
		Task<User> CreateOneAsync(User user);
		Task<User> DeleteOneAsync(User user);
		Task DeleteAllAsync();
	}
}