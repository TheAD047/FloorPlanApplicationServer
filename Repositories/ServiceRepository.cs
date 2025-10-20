using FloorPlanApplication.Data;
using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace FloorPlanApplication.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDBContext _context;

        public ServiceRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddService(Service Service)
        {
            _context.Add(Service);
            return Save();
        }

        public bool DeleteService(Service Service)
        {
            _context.Remove(Service);
            return Save();
        }

        public async Task<IEnumerable<Service>> GetAllActiveServices(int index, int number)
        {
            return await _context.Services
                            .Where(s => s.IsActive)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetAllCompletedServices(int index, int number)
        {
            return await _context.Services
                            .Where(s => s.IsCompleted)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetAllServices(int index, int number)
        {
            return await _context.Services                            
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<Service> GetServiceByID(int ID)
        {
            return await _context.Services
                            .FirstOrDefaultAsync(s => s.ID == ID);
        }

        public async Task<IEnumerable<Service>> GetServicesByClientID(string ClientID, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.ClientID.Equals(ClientID))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByCompanyID(int CompanyID, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.CompanyID == CompanyID)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByDateRange(DateTime min, DateTime max, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.ServiceRequestDate >= min && s.ServiceRequestDate <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByDescriptionMatching(string description, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.Description.Contains(description))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByEmployeeID(string EmployeeID, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.EmployeeID.Equals(EmployeeID))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByOtherServiceType(string parum, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.OtherServiceTypeName.Contains(parum))
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByServiceType(ServiceType serviceType, int index, int number)
        {
            return await _context.Services
                            .Where(s => s.ServiceType == serviceType)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false;
        }

        public bool UpdateService(Service Service)
        {
            _context.Update(Service);
            return Save();
        }
    }
}
