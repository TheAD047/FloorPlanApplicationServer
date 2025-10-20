using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDBContext _context;

        public OrderItemRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddOrderItem(OrderItem OrderItem)
        {
            _context.Add(OrderItem);
            return Save();
        }

        public bool DeleteOrderItem(OrderItem OrderItem)
        {
            _context.Remove(OrderItem);
            return Save();
        }

        public async Task<IEnumerable<OrderItem>> GetItemsByClientID(string ClientID, int index, int number)
        {
            return await _context.OrderItems
                            .Where(o => o.ClietnID.Equals(ClientID))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetItemsInOrder(int OrderID)
        {
            return await _context.OrderItems
                            .Where(o => o.OrderID == OrderID)
                            .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByID(int ID)
        {
            return await _context.OrderItems
                            .FirstOrDefaultAsync(o => o.ID == ID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false; 
        }

        public bool UpdateOrderItem(OrderItem OrderItem)
        {
            _context.Update(OrderItem);
            return Save();
        }
    }
}
