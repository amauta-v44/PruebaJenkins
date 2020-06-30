using System.ComponentModel.DataAnnotations;
using Devsoft.Api.Entities;

namespace Devsoft.Api.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Username = Username,
                Password = Password
            };
        }
    }
}