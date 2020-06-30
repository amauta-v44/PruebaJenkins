using System.ComponentModel.DataAnnotations;

namespace Devsoft.Api.Dtos.FoodGroup
{
    public class CreateFoodGroupDto
    {
        [Required]
        public string Name { get; set; }


        public Entities.FoodGroup ToEntity()
        {
            return new Entities.FoodGroup()
            {
                Name = Name
            };
        }
    }
}