using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllServices(int index, int number);
        Task<Service> GetServiceByID(int ID);
        Task<IEnumerable<Service>> GetServicesByClientID(string ClientID, int index, int number);
        Task<IEnumerable<Service>> GetServicesByEmployeeID(string EmployeeID, int index, int number);
        Task<IEnumerable<Service>> GetServicesByCompanyID(int CompanyID, int index, int number);
        Task<IEnumerable<Service>> GetServicesByDateRange(DateTime min, DateTime max, int index, int number);
        Task<IEnumerable<Service>> GetServicesByServiceType(ServiceType serviceType, int index, int number);
        Task<IEnumerable<Service>> GetServicesByOtherServiceType(string parum, int index, int number);
        Task<IEnumerable<Service>> GetAllActiveServices(int index, int number);
        Task<IEnumerable<Service>> GetAllCompletedServices(int index, int number);
        Task<IEnumerable<Service>> GetServicesByDescriptionMatching(string description, int index, int number);
        bool AddService(Service Service);
        bool UpdateService(Service Service);
        bool DeleteService(Service Service);
        bool Save();
    }
}
