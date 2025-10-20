using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloorPlanApplication.Models
{
    public class Photo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string URL { get; set; }

        public int PlanID { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("PlanID")]
        public virtual Plan? Plan { get; set; }
    }
}
