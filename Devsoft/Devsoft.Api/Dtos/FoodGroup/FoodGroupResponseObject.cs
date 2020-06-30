namespace Devsoft.Api.Dtos.FoodGroup
{
	public class FoodGroupResponseObject
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public static FoodGroupResponseObject FromEntity(Entities.FoodGroup foodGroup)
		{
			return new FoodGroupResponseObject
			{
				Id = foodGroup.Id,
				Name = foodGroup.Name
			};
		}
	}
}