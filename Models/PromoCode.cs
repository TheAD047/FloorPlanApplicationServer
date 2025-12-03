using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FloorPlanApplication.Models
{
    public class PromoCode
    {
        [Key]
        public Guid ID { get; set; }

        public string Code { get; set; }

        [AllowNull]
        [Range(1, 100)]
        public int? Percent { get; set; }

        [AllowNull]
        [Range(1, 100000)]
        public double? Amount { get; set; }

        public DateTime CreatedAtDateTime { get; set; } = DateTime.Now;

        [AllowNull]
        public DateTime? ExpiryDateTime { get; set; } = DateTime.Now;

        public bool IsLocedToAdmin { get; set; } = true;

        public bool IsExpires { get; set; } = false;
    }
}
