namespace FloorPlanApplication.Dtos.OrderItem
{
    public class EditItemDTO
    {
        public int OrderItemID { get; set; }
        public string? Code { get; set; }

        //temporary var
        public double Price { get; set; }
    }
}
