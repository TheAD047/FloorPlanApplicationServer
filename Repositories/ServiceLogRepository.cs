using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class ServiceLogRepository : IServiceLogRepository
    {
        private readonly AppDBContext _context;

        public ServiceLogRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddLog(ServiceLog log)
        {
            _context.Add(log);
            return Save();
        }

        public bool DeleteLog(ServiceLog log)
        {
            _context.Remove(log);
            return Save();
        }

        public async Task<IEnumerable<ServiceLog>> GetAllLogsByDateRange(DateTime min, DateTime max, int index, int number)
        {
            return await _context.ServicesLogs
                            .Where(s => s.LogDateTime >= min && s.LogDateTime <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<ServiceLog>> GetAllLogsByDay(DateTime day, int index, int number)
        {
            return await _context.ServicesLogs
                            .Where(s => s.LogDateTime.Date == day.Date)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<ServiceLog>> GetAllLogsByEmployeeID(string EmployeeID, int index, int number)
        {
            return await _context.ServicesLogs
                            .Where(s => s.EmployeeID.Equals(EmployeeID))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<ServiceLog>> GetAllLogsByServiceID(int ServiceID, int index, int number)
        {
            return await _context.ServicesLogs
                            .Where(s => s.ServiceID == ServiceID)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<ServiceLog> GetLogByID(int ID)
        {
            return await _context.ServicesLogs
                            .FirstOrDefaultAsync(s => s.ID == ID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false;
        }

        public bool UpdateLog(ServiceLog log)
        {
            _context.Update(log);
            return Save();
        }
    }
}
