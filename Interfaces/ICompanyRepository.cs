using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies(int index, int number);
        Task<Company> GetCompanyByID(int ID);
        Task<Company> GetCompanyByPhoneNumber(string phoneNumber);
        Task<IEnumerable<Company>> GetCompanyByName(string name);
        bool AddCompany(Company Company);
        bool UpdateCompany(Company Company);
        bool DeleteCompany(Company Company);
        bool Save();
    }
}
