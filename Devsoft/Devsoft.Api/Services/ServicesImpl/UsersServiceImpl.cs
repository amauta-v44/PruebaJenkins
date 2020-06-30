using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Services.ServicesImpl
{
	public class UsersServiceImpl : IUsersService
	{
		private readonly IUsersRepository _usersRepository;

		public UsersServiceImpl(
			IUsersRepository usersRepository
		)
		{
			_usersRepository = usersRepository;
		}

		
		public async Task<User> FindOneByIdAsync(int id)
		{
			var found = await _usersRepository.FindOneByIdAsync(id);

			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={id} not found");

			return found;
		}


		public async Task<ICollection<User>> FindAllAsync()
		{
			return await _usersRepository.FindAllAsync();
		}

		
		public async Task<User> CreateOneAsync(User t)
		{
			var user = await _usersRepository.FindOneByUsernameAsync(t.Username);

			if (user != null)
				throw new HttpResponseException(HttpStatusCode.BadRequest,
					$"User with username={t.Username} already exists");

			await _usersRepository.CreateOneAsync(t);
			return t;
		}

		
		public async Task<User> UpdateOneAsync(int id, User t)
		{
			throw new System.NotImplementedException();
		}

		
		public async Task<User> DeleteOneByIdAsync(int id)
		{
			var user = await _usersRepository.FindOneByIdAsync(id);

			if (user == null)
				throw new HttpResponseException(HttpStatusCode.NotFound, $"User with id={id} was not found");

			return user;
		}

		
		public async Task DeleteAllAsync()
		{
			await _usersRepository.DeleteAllAsync();
		}
	}
}