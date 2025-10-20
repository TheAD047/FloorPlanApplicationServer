using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Models
{
    public class ServiceLog
    {
        [Key]
        public int ID { get; set; } = 0;

        public int ServiceID { get; set; } = 0;

        [Required]
        [MaxLength(4000)]
        public string ServiceNotes { get; set; } = string.Empty;

        [Required]
        public string EmployeeID { get; set; } 

        [Required]
        public DateTime LogDateTime { get; set; } = DateTime.Now;

        [ForeignKey("ServiceID")]
        public virtual Service? Service { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual User? Employee { get; set; }
    }
}
