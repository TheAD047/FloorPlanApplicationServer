using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface IServiceLogRepository
    {
        Task<IEnumerable<ServiceLog>> GetAllLogsByServiceID(int ServiceID, int index, int number);
        Task<IEnumerable<ServiceLog>> GetAllLogsByEmployeeID(string EmployeeID, int index, int number);
        Task<IEnumerable<ServiceLog>> GetAllLogsByDay(DateTime day, int index, int number);
        Task<IEnumerable<ServiceLog>> GetAllLogsByDateRange(DateTime min, DateTime max, int index, int number);
        Task<ServiceLog> GetLogByID(int ID);
        bool AddLog(ServiceLog log);
        bool UpdateLog(ServiceLog log);
        bool DeleteLog(ServiceLog log);
        bool Save();
    }
}
