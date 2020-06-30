using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devsoft.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Devsoft.Api.Repositories.RepositoryImpl
{
	public class PublicationsRepositoryImpl : IPublicationsRepository
	{
		private readonly DevsoftContext _context;

		public PublicationsRepositoryImpl(
			DevsoftContext context
		)
		{
			_context = context;
		}


		public async Task<ICollection<Publication>> FindAllAsync()
		{
			return await _context
				.Publications
				.AsNoTracking()
				.Include(x => x.Food)
				.ToListAsync();
		}


		public async Task<ICollection<Publication>> FindAllByUserIdAsync(int userId)
		{
			return await _context
				.Publications
				.AsNoTracking()
				.Include(x => x.Food)
				.Where(x => x.UserId == userId)
				.ToListAsync();
		}


		public async Task<Publication> FindOneByIdAsync(int id)
		{
			return await _context
				.Publications
				.AsNoTracking()
				.Include(x => x.Food)
				.FirstOrDefaultAsync(x => x.Id == id);
		}


		public async Task<Publication> FindOneByUserIdAndFoodIdAndDescriptionAsync(int userId, int foodId,
			string description)
		{
			return await _context
				.Publications
				.AsNoTracking()
				.FirstOrDefaultAsync(x =>
					x.UserId == userId &&
					x.FoodId == foodId &&
					x.Description == description
				);
		}


		public async Task<Publication> CreateOneAsync(Publication publication)
		{
			await _context.Publications.AddAsync(publication);
			await _context.SaveChangesAsync();
			_context.Entry(publication).State = EntityState.Detached;
			return publication;
		}


		public async Task<Publication> UpdateOneAsync(Publication publication)
		{
			_context.Publications.Update(publication);
			await _context.SaveChangesAsync();
			_context.Entry(publication).State = EntityState.Detached;
			return publication;
		}


		public async Task<Publication> DeleteOneAsync(Publication publication)
		{
			_context.Publications.Remove(publication);
			await _context.SaveChangesAsync();
			_context.Entry(publication).State = EntityState.Detached;
			return publication;
		}


		public async Task DeleteAllAsync()
		{
			var items = await _context
				.Publications
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