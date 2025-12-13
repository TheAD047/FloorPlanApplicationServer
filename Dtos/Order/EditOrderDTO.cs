using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Dtos.Order
{
    public class EditOrderDTO
    {
        [Required]
        public int OrderID { get; set; }
        [AllowNull]
        public bool? IsFulfilled { get; set; }
        [AllowNull]
        public bool IsPlaced { get; set; } = false;
        [AllowNull]
        public bool? IsPaid { get; set; }
        [AllowNull]
        public bool? IsVerififed { get; set; }
        [AllowNull]
        public bool? IsTaxExempted { get; set; }
        [AllowNull]
        public bool? IsCancelled { get; set; }
        [AllowNull]
        public double? Discount { get; set; }
    }
}
