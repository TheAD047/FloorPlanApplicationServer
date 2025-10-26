using FloorPlanApplication.Dtos.Photo;
using FloorPlanApplication.Dtos.Plan;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class PlanMapper
    {
        public static PlanDTO ToPlanDTO(this Plan plan)
        {
            return new PlanDTO
            {
                Price = plan.Price,
                Title = plan.Title,
                Description = plan.Description,
                Bedrooms = plan.Bedrooms,
                Bathrooms = plan.Bathrooms,
                Area = plan.Area
            };
        }

        public static PlanDetailsDTO ToPlanDetailsDTO(this Plan plan)
        {
            return new PlanDetailsDTO
            {
                Price = plan.Price,
                Title = plan.Title,
                Description = plan.Description,
                Bedrooms = plan.Bedrooms,
                Bathrooms = plan.Bathrooms,
                Area = plan.Area,
                PhotoURLs = plan.Photos.Select(p => p.URL)
            };
        }

        public static Plan ToPlanFromCreateDTO(this AddPlanDTO DTO)
        {
            return new Plan
            {
                Price = DTO.Price,
                Title = DTO.Title,
                Description = DTO.Description,
                Bedrooms = DTO.Bedrooms,
                Bathrooms = DTO.Bathrooms,
                Area = DTO.Area
            };
        }
    }
}
