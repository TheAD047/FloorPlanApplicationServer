using FloorPlanApplication.Data.Enums;

namespace FloorPlanApplication.Dtos.Service
{
    public class AdminEditServiceDTO
    {
        public int ServiceID { get; set; }
        public string? EmployeeUsername { get; set; }
        public ServiceType? ServiceType { get; set; }
        public int? CompanyID { get; set; }
        public string? OtherServiceTypeName { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
