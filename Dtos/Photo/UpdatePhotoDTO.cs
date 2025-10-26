namespace FloorPlanApplication.Dtos.Photo
{
    public class UpdatePhotoDTO
    {
        public string URL { get; set; }

        public int PlanID { get; set; }

        public bool IsDeleted { get; set; }
    }
}
