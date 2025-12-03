using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.OrderItem
{
    public class OrderItemDTO
    {
        public double Price { get; set; } 
        public int PlanID { get; set; }

        public int? OrderID { get; set; }
    }
}
