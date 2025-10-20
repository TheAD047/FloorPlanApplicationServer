using FloorPlanApplication.Models;
using FloorPlanApplication.OptionModels;

namespace FloorPlanApplication.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetFullfilledOrders(int index, int number);
        Task<IEnumerable<Order>> GetPendingOrders(int index, int number);
        Task<IEnumerable<Order>> GetCommercialOrders(int index, int number);
        Task<IEnumerable<Order>> GetUnPaidOrders(int index, int number);
        Task<IEnumerable<Order>> GetPaidOrders(int index, int number);
        Task<IEnumerable<Order>> GetTaxExemptedOrders(int index, int number);
        Task<IEnumerable<Order>> GetOrdersSortedByTime(int index, int number);
        Task<IEnumerable<Order>> GetOrdersByFullfillmentDate(DateTime day, int index, int number);
        Task<IEnumerable<Order>> GetOrdersByVerificationDate(DateTime day, int index, int number);
        Task<IEnumerable<Order>> GetOrdersByPlacementDate(DateTime day, int index, int number);
        Task<IEnumerable<Order>> GetOrdersByPaymentDate(DateTime day, int index, int number);
        Task<IEnumerable<Order>> GetOrdersByDay(DateTime day, int index, int number);
        Task<Order> GetOrderByID(int ID);
        bool AddOrder(Order Order);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(Order Order);
        bool Save();
    }
}
