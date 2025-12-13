using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Dtos.Order
{
    public class OrderDetailsDTO
    {
        public int ID { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public DateTime? OrderFulfillmentDate { get; set; }
        public DateTime? OrderPlacementDate { get; set; }
        public DateTime? OrderVerficationDate { get; set; }
        public DateTime? OrderPaymentDate { get; set; }
        public DateTime? OrderCancellationDate { get; set; }
        public bool IsFulfilled { get; set; }
        public bool IsPaid { get; set; }
        public bool IsVerififed { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsTaxExempted { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCommercialOrder { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }
}
