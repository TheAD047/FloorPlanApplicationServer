using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Extensions;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanApplication.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/ServiceLogs")]
    public class ServiceLogController : ControllerBase
    {
        private readonly IServiceLogRepository _serviceLogRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly UserManager<User> _userManager;

        public ServiceLogController(IServiceRepository serviceRepository, IServiceLogRepository serviceLogRepository, UserManager<User> userManager)
        {
            _serviceLogRepository = serviceLogRepository;
            _serviceRepository = serviceRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("EmployeeServiceLogs")]
        public async Task<IActionResult> GetLogsByEmployee(string employeeUsername, int? index)
        {
            if (employeeUsername.Length == 0)
                return BadRequest();

            var username = User.GetUsername();

            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
                return Unauthorized();

            bool isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            if (user.Email != employeeUsername && !isAdmin)
                return Unauthorized();

            bool isEmployee = await _userManager.IsInRoleAsync(user, UserRole.EMPLOYEE.ToString());

            if (!isAdmin && !isEmployee)
                return Unauthorized();

            var logs = await _serviceLogRepository.GetAllLogsByEmployeeID(employeeUsername, index ?? 0, 20);

            return Ok(logs);
        }

        [HttpDelete]
        [Route("DeleteLog")]
        public async Task<IActionResult> DeleteLog([FromRoute] int LogID)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            bool isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            if(!isAdmin)
                return Unauthorized();

            var log = await _serviceLogRepository.GetLogByID(LogID);

            if (log == null)
                return NotFound();

            bool deleted = _serviceLogRepository.DeleteLog(log);

            if (!deleted)
                return StatusCode(500);

            return NoContent();
        }

    }
}
