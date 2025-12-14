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

        public static Service ToServiceFromAdminEditDTO(this AdminEditServiceDTO DTO, Service service)
        {
            Service newService = new Service
            {
                ID = DTO.ServiceID,
                EmployeeID = DTO.EmployeeUsername,
                ServiceType = DTO.ServiceType ?? service.ServiceType,
                CompanyID = DTO.CompanyID,
                OtherServiceTypeName= DTO.OtherServiceTypeName,
                Description = DTO.Description,
            };

            if(newService.IsAccepted && !service.IsAccepted)
            {
                newService.IsAccepted = true;
                newService.ServiceAcceptanceDate = DateTime.Now;
            }

            if(newService.IsCompleted && !service.IsCompleted)
            {
                newService.IsCompleted = true;
                newService.ServiceCompletionDate = DateTime.Now;
            }

            return newService;
        }
    }
}
