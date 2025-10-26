using FloorPlanApplication.Dtos.Photo;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class PhotoMapper
    {
        public static PhotoDTO ToPhotoDTO(this Photo photo)
        {
            return new PhotoDTO
            {
                URL = photo.URL,
                PlanID = photo.PlanID,
                IsDeleted = photo.IsDeleted
            };
        }

        public static Photo ToPhotoFromCreatePhotoDTO(this CreatePhotoDTO DTO)
        {
            return new Photo
            {
                URL = DTO.URL,
                PlanID = DTO.PlanID
            };
        }
    }
}
