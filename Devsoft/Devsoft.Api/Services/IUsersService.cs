using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Services
{
	public interface IUsersService
	{
		Task<ICollection<User>> FindAllAsync();
		Task<User> FindOneByIdAsync(int id);
		Task<User> CreateOneAsync(User user);
	}
}