using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.Photo
{
    public class PhotoDTO
    {
        public string URL { get; set; }

        public int PlanID { get; set; }

        public bool IsDeleted { get; set; } 
    }
}
