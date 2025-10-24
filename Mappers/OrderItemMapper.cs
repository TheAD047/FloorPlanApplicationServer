using FloorPlanApplication.Dtos.OrderItem;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemDTO ToOrderItemDTO(this OrderItem item)
        {
            return new OrderItemDTO
            {
                Price = item.Price,
                ClientID = item.ClientID,
                PlanID = item.PlanID,
                OrderID = item.OrderID
            };
        }

        public static OrderItem ToOrderItemFromCreatDTO(this CreateOrderItemDTO DTO)
        {
            return new OrderItem
            { 
                ClientID = DTO.ClientID,
                PlanID = DTO.PlanID,
                OrderID = DTO.OrderID
            };
        }
    }
}
