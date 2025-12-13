using FloorPlanApplication.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [AllowNull]
        public int? ComopanyID { get; set; }

        public UserRole UserRole { get; set; } = UserRole.TENTATIVE;

        [AllowNull]
        public int EmployeeNumber { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("CompanyID")]
        public virtual Company? Company { get; set; }
    }
}
