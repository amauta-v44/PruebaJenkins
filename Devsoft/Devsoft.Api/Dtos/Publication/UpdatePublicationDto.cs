using System;

namespace Devsoft.Api.Dtos.Publication
{
	public class UpdatePublicationDto
	{
		public string Description { get; set; }
		public int Stock { get; set; }
		public float PricePerUnit { get; set; }
		public bool IsPublished { get; set; }
		public DateTime StartDate { get; set; }
		// public DateTime EndDate { get; set; }
		public string ImageUrl { get; set; }
		public int FoodId { get; set; }

		
		public static Entities.Publication ToEntity(UpdatePublicationDto obj)
		{
			return new Entities.Publication
			{
				Description = obj.Description,
				Stock = obj.Stock,
				StartDate = obj.StartDate,
				// EndDate = obj.EndDate,
				ImageUrl = obj.ImageUrl,
				IsPublished = obj.IsPublished,
				PricePerUnit = obj.PricePerUnit,
				FoodId = obj.FoodId,
			};
		}

		public Entities.Publication ToEntity()
		{
			return new Entities.Publication
			{
				Description = this.Description,
				Stock = this.Stock,
				StartDate = this.StartDate,
				// EndDate = this.EndDate,
				ImageUrl = this.ImageUrl,
				IsPublished = this.IsPublished,
				PricePerUnit = this.PricePerUnit,
				FoodId = this.FoodId
			};
		}
	}
}