using System;
using System.ComponentModel.DataAnnotations;

namespace Devsoft.Api.Dtos.Publication
{
	public class CreatePublicationDto
	{
		[Required] public string Description { get; set; }

		[Required] public int Stock { get; set; }

		[Required] public float PricePerUnit { get; set; }

		[Required] public bool IsPublished { get; set; }

		[Required] public DateTime StartDate { get; set; }

		// [Required] public DateTime EndDate { get; set; }

		[Required] public string ImageUrl { get; set; }

		public int FoodId { get; set; }
		public int UserId { get; set; }
	}
}