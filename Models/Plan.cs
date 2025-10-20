using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Models
{
    public class Plan
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double Price { get; set; } = 0.01;

        [Required]
        [MaxLength(256)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(4096)]
        public string Description { get; set; } = string.Empty;
               
        [Required]
        public int Bedrooms { get; set; } = 1;

        [Required]
        public int Bathrooms { get; set; } = 1;

        [Required]
        public double Area { get; set; } = 0.01;

        [Required]
        public bool IsActive { get; set; } = true;

        public IEnumerable<Photo> Photos { get; set; } = new List<Photo>();
    }
}
