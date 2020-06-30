using System;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Dtos.Publication
{
	public class PublicationResponseObject
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public int Stock { get; set; }
		public float PricePerUnit { get; set; }
		public bool IsPublished { get; set; }
		public DateTime StartDate { get; set; }
		// public DateTime EndDate { get; set; }
		public string ImageUrl { get; set; }
		public int FoodId { get; set; }
		public int FoodGroupId { get; set; }

		public static PublicationResponseObject FromEntity(Entities.Publication publication)
		{
			return new PublicationResponseObject
			{
				Id = publication.Id,
				Description = publication.Description,
				Stock = publication.Stock,
				StartDate = publication.StartDate,
				// EndDate = publication.EndDate,
				ImageUrl = publication.ImageUrl,
				IsPublished = publication.IsPublished,
				PricePerUnit = publication.PricePerUnit,
				FoodId = publication.FoodId,
				FoodGroupId = publication.Food.FoodGroupId
			};
		}
	}
}