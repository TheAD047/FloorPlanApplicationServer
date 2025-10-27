using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.User
{
    public class RegisterDTO
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

    }
}
