using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDBContext _context;

        public PlanRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddPlan(Plan Plan)
        {
            _context.Add(Plan);
            return Save();
        }

        public bool DeletePlan(Plan Plan)
        {
            _context.Remove(Plan);
            return Save();
        }

        public async Task<Plan> GetPlanByID(int ID)
        {
            return await _context.Plans
                            .FirstOrDefaultAsync(p => p.ID == ID);
        }

        public async Task<IEnumerable<Plan>> GetPlans(int index, int number)
        {
            return await _context.Plans
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByArea(double min, double max, int index, int number)
        {
            return await _context.Plans
                            .Where(p => p.Area >= min && p.Area <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByNumberOfBathrooms(int min, int max, int index, int number)
        {
            return await _context.Plans
                            .Where(p => p.Bathrooms >= min && p.Bathrooms <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByNumberOfBedrooms(int min, int max, int index, int number)
        {
            return await _context.Plans
                            .Where(p => p.Bedrooms >= min && p.Bedrooms <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByPrice(double min, double max, int index, int number)
        {
            return await _context.Plans
                            .Where(p => p.Price >= min && p.Price <= max)
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 1 ? true : false;
        }

        public bool UpdatePlan(Plan Plan)
        {
            _context.Update(Plan);
            return Save();
        }
    }
}
