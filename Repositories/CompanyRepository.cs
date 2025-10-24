using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDBContext _context;

        public CompanyRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddCompany(Company Company)
        {
            _context.Add(Company);
            return Save();
        }

        public bool DeleteCompany(Company Company)
        {
            _context.Remove(Company);
            return Save();
        }

        public async Task<IEnumerable<Company>> GetCompanies(int index, int number)
        {
            return await _context.Companies
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        
        public async Task<Company> GetCompanyByID(int ID)
        {
            return await _context.Companies
                            .FirstOrDefaultAsync(c => c.ID == ID);
        }

        public async Task<IEnumerable<Company>> GetCompanyByName(string name)
        {
            return await _context.Companies
                            .Where(c => c.CompanyName.Contains(name))
                            .ToListAsync();
        }

        public async Task<Company> GetCompanyByPhoneNumber(string phoneNumber)
        {
            return await _context.Companies
                            .FirstOrDefaultAsync(c => c.PhoneNumber.Equals(phoneNumber));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCompany(Company Company)
        {
            _context.Update(Company);
            return Save();
        }
    }
}
