using FloorPlanApplication.Dtos.Company;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDTO ToCompanyDTO(this Company Company)
        {
            return new CompanyDTO 
            {
                ID = Company.ID,
                Address = Company.Address,
                PhoneNumber = Company.PhoneNumber,
                CompanyName = Company.CompanyName
            };

        }

        public static Company ToCompanyFromCreateDTO(this CreateCompanyDTO DTO)
        {
            return new Company
            { 
                Address = DTO.Address,
                PhoneNumber = DTO.PhoneNumber,
                CompanyName = DTO.CompanyName
            };

        }
    }
}
