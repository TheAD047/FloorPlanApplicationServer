using FloorPlanApplication.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Models
{
    public class Service
    {
        [Key]
        public int ID { get; set; } = 0;

        [Required]
        public ServiceType ServiceType { get; set; } = ServiceType.DEFAULT;

        [Required]
        public string ClientID { get; set; }

        [AllowNull]
        public int CompanyID { get; set; } 

        [AllowNull]
        public string EmployeeID { get; set; } 

        [AllowNull]
        public string OtherServiceTypeName { get; set; } = string.Empty;

        [Required]
        public DateTime ServiceRequestDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsCompleted { get; set; } = false;

        [ForeignKey("ClientID")]
        public virtual User? Client { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual User? Employee { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company? Company { get; set; }

        public IEnumerable<ServiceLog> Logs { get; set; } = new List<ServiceLog>();
    }
}
