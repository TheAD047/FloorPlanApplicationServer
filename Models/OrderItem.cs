using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FloorPlanApplication.Models
{
    public class OrderItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double Price { get; set; } = 0.01;

        [Required]
        public string ClientID { get; set; }

        [Required]
        public int PlanID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [ForeignKey("ClientID")]
        public virtual User? Client { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
    }
}
