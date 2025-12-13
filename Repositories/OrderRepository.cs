using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;

        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddOrder(Order Order)
        {
            _context.Add(Order);
            return Save();
        }

        public bool DeleteOrder(Order Order)
        {
            _context.Remove(Order);
            return Save();
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientID(string ClientID, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.ClientID == ClientID)
                            .Skip(index *  number)
                            .Take(number)
                            .ToListAsync();
        }


        public async Task<IEnumerable<Order>> GetCommercialOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.IsCommercialOrder)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetFullfilledOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.IsFulfilled)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<Order> GetOrderByID(int ID)
        {
            return await _context.Orders
                            .FirstOrDefaultAsync(o => o.ID == ID);
        }

        public async Task<IEnumerable<Order>> GetOrdersByDay(DateTime day, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.OrderCreationDate.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByFullfillmentDate(DateTime day, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.OrderFulfillmentDate.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByPaymentDate(DateTime day, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.OrderPaymentDate.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByPlacementDate(DateTime day, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.OrderPlacementDate.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByVerificationDate(DateTime day, int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.OrderVerficationDate.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersSortedByTime(int index, int number)
        {
            return await _context.Orders
                            .Skip(index * number)
                            .Take(number)
                            .OrderBy(o => o.OrderCreationDate)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetPaidOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.IsPaid)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetPendingOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => !o.IsFulfilled)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetTaxExemptedOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => o.IsTaxExempted)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetUnPaidOrders(int index, int number)
        {
            return await _context.Orders
                            .Where(o => !o.IsPaid)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 1 ? true : false;
        }

        public bool UpdateOrder(Order Order)
        {
            _context.Update(Order);
            return Save();
        }
    }
}
