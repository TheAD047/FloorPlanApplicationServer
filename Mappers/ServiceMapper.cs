using FloorPlanApplication.Dtos.Service;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceDTO ToServiceDTO(this Service service)
        {
            return new ServiceDTO
            {
                ID = service.ID,
                ServiceType = service.ServiceType,
                CompanyID = service.CompanyID,
                OtherServiceTypeName = service.OtherServiceTypeName,
                ServiceRequestDate = service.ServiceRequestDate,
                Description = service.Description,
                IsActive = service.IsActive,
                IsCompleted = service.IsCompleted,
            };
        }

        public static Service ToServiceFromCreateDTO(this CreateServiceDTO DTO)
        {
            return new Service
            {
                ServiceType = DTO.ServiceType,
                OtherServiceTypeName = DTO.OtherServiceTypeName,
                Description = DTO.Description,
            };
        }
    }
}
