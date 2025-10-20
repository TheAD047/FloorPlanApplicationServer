using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetItemsInOrder(int OrderID);
        Task<IEnumerable<OrderItem>> GetItemsByClientID(string ClientID, int index, int number);
        Task<OrderItem> GetOrderItemByID(int ID);
        bool AddOrderItem(OrderItem OrderItem);
        bool UpdateOrderItem(OrderItem OrderItem);
        bool DeleteOrderItem(OrderItem OrderItem);
        bool Save();
    }
}
