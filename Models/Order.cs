using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public DateTime OrderCreationDate { get; set; } = DateTime.Now;

        [AllowNull]
        public DateTime OrderFulfillmentDate { get; set; }

        [AllowNull]
        public DateTime OrderPlacementDate { get; set; }

        [AllowNull]
        public DateTime OrderVerficationDate { get; set; }

        [AllowNull]
        public DateTime OrderPaymentDate { get; set; }

        [AllowNull]
        public DateTime OrderCancellationDate { get; set; }

        public bool IsFulfilled { get; set; } = false;

        public bool IsPaid { get; set; } = false;

        public bool IsVerififed { get; set; } = false;

        public bool IsPlaced { get; set; } = false;

        public bool IsTaxExempted { get; set; } = false;

        public bool IsCancelled { get; set; } = false;

        public bool IsCommercialOrder => !Client.ComopanyID.Equals(null);

        public double SubTotal { get; set; } = 0.01;

        public double Tax { get; set; } = 0.01;

        public double Total { get; set; } = 0.01;

        [Required]
        public string ClientID { get; set; }

        [AllowNull]
        public int? CompanyID { get; set; } = null;

        [AllowNull]
        public double Discount { get; set; }

        [ForeignKey("ClientID")]
        public virtual User? Client { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company? Company { get; set; }

        public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
