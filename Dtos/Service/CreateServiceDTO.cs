using FloorPlanApplication.Data.Enums;

namespace FloorPlanApplication.Dtos.Service
{
    public class CreateServiceDTO
    {
        public ServiceType ServiceType { get; set; } 
        public string? OtherServiceTypeName { get; set; }
        public string Description { get; set; }
    }
}
