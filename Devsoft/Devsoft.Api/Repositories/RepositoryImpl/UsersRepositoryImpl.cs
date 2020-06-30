using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Repositories.RepositoryImpl
{
	public class UsersRepositoryImpl : IUsersRepository
	{
		private readonly DevsoftContext _context;

		public UsersRepositoryImpl(
			DevsoftContext context)
		{
			_context = context;
		}


		public async Task<ICollection<User>> FindAllAsync()
		{
			return await _context
				.Users
				.AsNoTracking()
				.ToListAsync();
		}


		public async Task<User> FindOneByIdAsync(int id)
		{
			return await _context
				.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}


		public async Task<User> FindOneByUsernameAsync(string username)
		{
			return await _context
				.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Username == username);
		}


		public async Task<User> CreateOneAsync(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			_context.Entry(user).State = EntityState.Detached;
			return user;
		}

		public async Task<User> DeleteOneAsync(User user)
		{
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			_context.Entry(user).State = EntityState.Detached;
			return user;
		}

		public async Task DeleteAllAsync()
		{
			var items = await _context.Users.AsNoTracking().ToListAsync();

			foreach (var item in items)
			{
				_context.Remove(item);
			}

			await _context.SaveChangesAsync();
		}
	}
}