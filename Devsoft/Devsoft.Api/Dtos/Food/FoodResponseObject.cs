namespace Devsoft.Api.Dtos.Food
{
	public class FoodResponseObject
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int FoodGroupId { get; set; }

		public static FoodResponseObject FromEntity(Entities.Food entity)
		{
			return new FoodResponseObject
			{
				Id = entity.Id,
				Name = entity.Name,
				FoodGroupId = entity.FoodGroupId
			};
		}
	}
}