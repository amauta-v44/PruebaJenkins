using System.ComponentModel.DataAnnotations;

namespace Devsoft.Api.Dtos.Food
{
    public class CreateFoodDto
    {
        [Required] public string Name { get; set; }

        [Required] public int FoodGroupId { get; set; }

        public Entities.Food ToEntity()
        {
            return new Entities.Food()
            {
                Name = Name,
            };
        }
    }
}