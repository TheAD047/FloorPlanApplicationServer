using FloorPlanApplication.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.Service
{
    public class ServiceDTO
    {
        public int ID { get; set; } 
        public ServiceType ServiceType { get; set; } 
        public int? CompanyID { get; set; }
        public string? OtherServiceTypeName { get; set; }
        public DateTime ServiceRequestDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }
    }
}
