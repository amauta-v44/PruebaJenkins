using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Devsoft.Api.Dtos.Food;

namespace Devsoft.Api.Entities
{
	public class Food
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }


		// Relations
		public IList<Publication> Publications { get; set; }
		public int FoodGroupId { get; set; }
		public FoodGroup FoodGroup { get; set; }

		public override string ToString()
		{
			return $"Food {{ Id={Id}, Name={Name} }}";
		}
	}
}