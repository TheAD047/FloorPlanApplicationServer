using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Dtos.User;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanApplication.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = new User 
                { 
                    Email = DTO.Email,
                    UserName = DTO.Email,
                    FirstName = DTO.FirstName,
                    LastName = DTO.LastName,
                    UserRole = UserRole.CLIENT_REGULAR,
                    IsActive = true,
                    EmailConfirmed = true,
                    PhoneNumber = "111-111-1111",
                    PhoneNumberConfirmed = true
                };

                var createdUser = await _userManager.CreateAsync(newUser, DTO.Password);
                
                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(newUser, newUser.UserRole.ToString());
                    if(roleResult.Succeeded)
                    {
                        return Ok(new
                        {
                            Email = newUser.Email,
                            Token = _tokenService.CreateToken(newUser)
                        });

                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
