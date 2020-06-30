using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Devsoft.Api.Dtos.Users;

namespace Devsoft.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relations
        public IList<Publication> Publications { get; set; }


        public override string ToString()
        {
            return $"User {{ Username={Username}, Password=******** }}";
        }

        public UserResponseObject ToResponseObject()
        {
            return new UserResponseObject
            {
                Id = Id,
                Username = Username
            };
        }
    }
}