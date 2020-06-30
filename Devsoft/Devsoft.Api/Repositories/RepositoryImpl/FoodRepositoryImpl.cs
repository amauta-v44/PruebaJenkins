using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Repositories.RepositoryImpl
{
	public class FoodRepositoryImpl : IFoodRepository
	{
		private readonly DevsoftContext _context;

		public FoodRepositoryImpl(
			DevsoftContext context
		)
		{
			_context = context;
		}

		public async Task<IEnumerable<Food>> FindAllAsync()
		{
			return await _context
				.Food
				.AsNoTracking()
				.Include(x => x.FoodGroup)
				.ToListAsync();
		}

		public async Task<Food> FindOneByIdAsync(int id)
		{
			return await _context
				.Food
				.AsNoTracking()
				.Include(x => x.FoodGroup)
				.FirstOrDefaultAsync(x => x.Id == id);
		}


		public async Task<Food> FindOneByNameAsync(string name)
		{
			return await _context
				.Food
				.AsNoTracking()
				.Include(x => x.FoodGroup)
				.FirstOrDefaultAsync(x => x.Name == name);
		}


		public async Task<Food> CreateOneAsync(Food food)
		{
			await _context.Food.AddAsync(food);
			await _context.SaveChangesAsync();
			_context.Entry(food).State = EntityState.Detached;
			return food;
		}

		public async Task<Food> UpdateOneAsync(Food food)
		{
			_context.Food.Update(food);
			await _context.SaveChangesAsync();
			_context.Entry(food).State = EntityState.Detached;
			return food;
		}

		public async Task<Food> DeleteOneAsync(Food food)
		{
			_context.Food.Remove(food);
			await _context.SaveChangesAsync();
			_context.Entry(food).State = EntityState.Detached;
			return food;
		}


		public async Task DeleteAllAsync()
		{
			var items = await _context
				.Food
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