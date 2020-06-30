using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devsoft.Api.Dtos.Publication;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Services
{
	public interface IPublicationService
	{
		Task<IEnumerable<PublicationResponseObject>> FindAllAsync();
		Task<PublicationResponseObject> FindOneByIdAsync(int id);
		Task<IEnumerable<PublicationResponseObject>> FindAllByUserIdAsync(int userId);
		Task<PublicationResponseObject> CreateOneAsync(CreatePublicationDto t);
		Task<PublicationResponseObject> UpdateOneByIdAsync(int id, UpdatePublicationDto t);
		Task<PublicationResponseObject> DeleteOneByIdAsync(int id);
	}
}