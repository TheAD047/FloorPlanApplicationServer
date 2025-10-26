using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Dtos.Photo
{
    public class CreatePhotoDTO
    {
        public string URL { get; set; }

        public int PlanID { get; set; }
    }
}
