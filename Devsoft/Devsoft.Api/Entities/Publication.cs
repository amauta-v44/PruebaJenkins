using System;
using System.ComponentModel.DataAnnotations;
using Devsoft.Api.Dtos.Publication;

namespace Devsoft.Api.Entities
{
	public class Publication
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public int Stock { get; set; }
		public float PricePerUnit { get; set; }
		public int Unit { get; set; }
		public bool IsPublished { get; set; }
		public DateTime StartDate { get; set; }
		// public DateTime EndDate { get; set; }
		public string ImageUrl { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Relations
		public int FoodId { get; set; }
		public Food Food { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }
	}
}