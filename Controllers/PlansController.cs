using FloorPlanApplication.Dtos.Plan;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FloorPlanApplication.Controllers
{
    [Route("api/Plans")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanRepository _planRepository;
        private readonly IPhotoRepository _photoRepository;

        public PlansController(IPlanRepository planRepository, IPhotoRepository photoRepository)
        {
            _planRepository = planRepository;
            _photoRepository = photoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? index)
        {
            var list = await _planRepository.GetPlans(index ?? 0, 10);

            var Plans = list.Select(p => p.ToPlanDTO());

            return Ok(Plans);
        }

        [HttpPost]
        [Route("AddPlan")]
        public async Task<IActionResult> AddPlan([FromBody] AddPlanDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Plan plan = DTO.ToPlanFromCreateDTO();

            bool added = _planRepository.AddPlan(plan);

            if (!added)
                return BadRequest();

            return CreatedAtAction(nameof(GetPlanDetails), new { ID = plan.ID }, plan.ToPlanDTO());
        }

        [HttpGet("{ID:int}")]
        public async Task<IActionResult> GetPlanDetails([FromRoute] int ID)
        {
            Plan plan = await _planRepository.GetPlanByID(ID);

            if (plan == null)
                return NotFound();

            return Ok(plan.ToPlanDetailsDTO());
        }

        [HttpDelete]
        [Route("DeletePlan/{ID:int}")]
        public async Task<IActionResult> DeletePlan([FromRoute] int ID, bool purge)
        {
            Plan plan = await _planRepository.GetPlanByID(ID);

            if (plan == null)
                return NotFound();

            if (!plan.IsActive && !purge)
                return BadRequest();

            plan.IsActive = false;

            if(!purge)
            {
                bool saved = _planRepository.UpdatePlan(plan);

                if (!saved)
                    return BadRequest();

                return Ok(plan.ToPlanDTO());
            }

            bool deleted = _planRepository.DeletePlan(plan);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }

        [HttpPut]
        [Route("UpdatePlan/{ID:int}")]
        public async Task<IActionResult> GetPlansByArea([FromRoute] int ID, [FromBody] UpdatePlanDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Plan plan = await _planRepository.GetPlanByID(ID);

            if (plan == null)
                return NotFound();

            plan.Price = DTO.Price;
            plan.Title = DTO.Title;
            plan.Description = DTO.Description;
            plan.Bedrooms = DTO.Bedrooms;
            plan.Bathrooms = DTO.Bathrooms;
            plan.Area = DTO.Area;
            plan.IsActive = DTO.IsActive;

            bool saved = _planRepository.UpdatePlan(plan);

            if (!saved)
                return BadRequest();

            return Ok(plan.ToPlanDTO());
        }

        [HttpGet]
        [Route("GetPlansByBedrooms")]
        public async Task<IActionResult> GetPlansByBedrooms([FromBody] PlanLookupByBedroomsDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var list = await _planRepository.GetPlansByNumberOfBedrooms(DTO.min, DTO.max, DTO.index ?? 0, 10);

            if (list.Count() == 0)
                return NotFound();

            var plasn = list.Select(p => p.ToPlanDTO());

            return Ok(plasn);
        }

        [HttpGet]
        [Route("GetPlansByBathrooms")]
        public async Task<IActionResult> GetPlansByBathrooms([FromBody] PlanLookupByBathroomDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var list = await _planRepository.GetPlansByNumberOfBathrooms(DTO.min, DTO.max, DTO.index ?? 0, 10);

            if (list.Count() == 0)
                return NotFound();

            var plasn = list.Select(p => p.ToPlanDTO());

            return Ok(plasn);
        }

        [HttpGet]
        [Route("GetPlanByPriceRange")]
        public async Task<IActionResult> GetPlanByPriceRange([FromBody] PlanLookupByPriceDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var list = await _planRepository.GetPlansByPrice(DTO.min, DTO.max, DTO.index ?? 0, 10);

            if (list.Count() == 0)
                return NotFound();

            var plasn = list.Select(p => p.ToPlanDTO());

            return Ok(plasn);
        }

        [HttpGet]
        [Route("GetPlanByArea")]
        public async Task<IActionResult> GetPlanByArea([FromBody] PlanLookupByAreaDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var list = await _planRepository.GetPlansByPrice(DTO.min, DTO.max, DTO.index ?? 0, 10);

            if (list.Count() == 0)
                return NotFound();

            var plasn = list.Select(p => p.ToPlanDTO());

            return Ok(plasn);
        }

    }
}
