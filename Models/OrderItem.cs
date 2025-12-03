using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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

        [AllowNull]
        public int? OrderID { get; set; }

        [ForeignKey("ClientID")]
        public virtual User? Client { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order? Order { get; set; }
    }
}
