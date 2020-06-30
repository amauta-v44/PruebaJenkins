using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Repositories.RepositoryImpl
{
	public class FoodGroupRepositoryImpl : IFoodGroupRepository
	{
		private readonly DevsoftContext _context;

		public FoodGroupRepositoryImpl(
			DevsoftContext context
		)
		{
			_context = context;
		}


		public async Task<ICollection<FoodGroup>> FindAllAsync()
		{
			return await _context
				.FoodGroups
				.AsNoTracking()
				.ToListAsync();
		}


		public async Task<FoodGroup> FindOneByIdAsync(int id)
		{
			return await _context
				.FoodGroups
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}


		public async Task<FoodGroup> FindOneByNameAsync(string name)
		{
			return await _context
				.FoodGroups
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Name == name);
		}


		public async Task<FoodGroup> CreateOneAsync(FoodGroup foodGroup)
		{
			await _context.FoodGroups.AddAsync(foodGroup);
			await _context.SaveChangesAsync();
			_context.Entry(foodGroup).State = EntityState.Detached;
			return foodGroup;
		}


		public async Task<FoodGroup> DeleteOneAsync(FoodGroup foodGroup)
		{
			_context.FoodGroups.Remove(foodGroup);
			await _context.SaveChangesAsync();
			_context.Entry(foodGroup).State = EntityState.Detached;
			return foodGroup;
		}


		public async Task DeleteAllAsync()
		{
			var items = await _context
				.FoodGroups
				.AsNoTracking()
				.ToListAsync();

			foreach (var item in items)
			{
				_context.Remove(item);
			}

			await _context.SaveChangesAsync();
		}
	}
}