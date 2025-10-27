using FloorPlanApplication.Dtos.Photo;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FloorPlanApplication.Controllers
{
    [Route("api/Photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IPlanRepository _planRepository;

        public PhotoController(IPhotoRepository photoRepository, IPlanRepository planRepository)
        {
            _photoRepository = photoRepository;
            _planRepository = planRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int? index)
        {
            var list = await _photoRepository.GetPhotosSortedByTime(index ?? 0, 10);

            var Photos = list.Select(p => p.ToPhotoDTO());

            return Ok(Photos);
        }

        [HttpPost]
        [Route("AddPhoto")]
        public async Task<IActionResult> AddPhoto([FromBody] CreatePhotoDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Plan plan = await _planRepository.GetPlanByID(DTO.PlanID);

            if (plan == null)
                return NotFound();

            //Check to see if Photo was uploaded to CDN server

            var photoCheck = await _photoRepository.GetPhotosForPlan(DTO.PlanID);

            if (photoCheck.Count() == 5)
                return BadRequest("The Plan already has the maximum number of photos attached");

            Photo photo = DTO.ToPhotoFromCreatePhotoDTO();

            bool saved = _photoRepository.AddPhoto(photo);

            if (!saved)
                return BadRequest();

            return CreatedAtAction(nameof(GetPhotoDetails), new { ID = photo.ID }, photo.ToPhotoDTO());
        }

        [HttpGet("{ID:int}")]
        public async Task<IActionResult> GetPhotoDetails([FromRoute] int ID)
        {
            Photo photo = await _photoRepository.GetPhotoByID(ID);

            if (photo == null)
                return NotFound();

            return Ok(photo.ToPhotoDTO());
        }

        [HttpPut]
        [Route("UpdatePhoto/{ID:int}")]
        public async Task<IActionResult> UpdatePhoto([FromRoute] int ID, [FromRoute] UpdatePhotoDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Photo photo = await _photoRepository.GetPhotoByID(ID);

            if (photo == null)
                return NotFound();

            photo.URL = DTO.URL;
            photo.PlanID = DTO.PlanID;
            photo.IsDeleted = DTO.IsDeleted;

            bool saved = _photoRepository.UpdatePhoto(photo);

            if (!saved)
                return BadRequest();

            return Ok(photo.ToPhotoDTO());
        }

        [HttpDelete]
        [Route("DeletePhoto/{ID:int}")]
        public async Task<IActionResult> DeletePhoto([FromRoute] int ID, [FromBody] bool purgePhoto)
        {
            Photo photo = await _photoRepository.GetPhotoByID(ID);

            if (photo == null)
                return NotFound();

            if (photo.IsDeleted && !purgePhoto)
                return BadRequest();

            photo.IsDeleted = true;

            if (!purgePhoto)
            {
                bool saved = _photoRepository.UpdatePhoto(photo);

                if (!saved)
                    return BadRequest();

                return Ok(photo.ToPhotoDTO());
            }

            bool deleted = _photoRepository.DeletePhotoReference(photo);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }

        [HttpGet]
        [Route("GetPhotosForPlan/{ID:int}")]
        public async Task<IActionResult> GetPhotosForPlan([FromRoute] int ID)
        {
            Plan plan = await _planRepository.GetPlanByID(ID);

            if (plan == null)
                return NotFound();

            var list = await _photoRepository.GetPhotosForPlan(ID);

            if (list.Count() == 0)
                return Ok("No Photos for this plan");

            var Photos = list.Select(p => p.ToPhotoDTO());

            return Ok(Photos);
        }
    }
}
