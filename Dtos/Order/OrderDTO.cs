using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Dtos.Order
{
    public class OrderDTO
    {
        public int ID { get; set; }

        public DateTime? OrderCreationDate { get; set; } 

        [AllowNull]
        public DateTime? OrderFulfillmentDate { get; set; }

        [AllowNull]
        public DateTime? OrderPlacementDate { get; set; }

        [AllowNull]
        public DateTime? OrderVerficationDate { get; set; }

        [AllowNull]
        public DateTime? OrderPaymentDate { get; set; }

        public bool? IsFulfilled { get; set; } 

        public bool? IsPaid { get; set; } 

        public bool? IsVerififed { get; set; }

        public bool? IsPlaced { get; set; }

        public bool? IsTaxExempted { get; set; }

        public bool? IsCommercialOrder { get; set; } 

        public double? SubTotal { get; set; }
        
        [AllowNull]
        public double? Tax { get; set; }

        [AllowNull]
        public int? CompanyID { get; set; }

        [AllowNull]
        public double? Discount { get; set; }
    }
}
