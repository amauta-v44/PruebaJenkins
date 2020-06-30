using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Devsoft.Api.Dtos.FoodGroup;

namespace Devsoft.Api.Entities
{
	public class FoodGroup
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// Relations
		public ICollection<Food> Food { get; set; }
	}
}