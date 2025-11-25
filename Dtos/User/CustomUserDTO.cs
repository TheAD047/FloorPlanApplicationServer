using FloorPlanApplication.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.User
{
    public class CustomUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public UserRole UserRole { get; set; }  
    }
}
