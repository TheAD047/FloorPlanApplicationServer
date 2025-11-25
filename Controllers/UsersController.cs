using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Dtos.User;
using FloorPlanApplication.Extensions;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newUser = new User 
                { 
                    Email = DTO.Email.ToLower(),
                    UserName = DTO.Email.ToLower(),
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

        [HttpPost]
        [Authorize]
        [Route("Admin/Register/ResgisterCustomUser")]
        public async Task<IActionResult> RegisterCustomUser(CustomUserDTO DTO)
        {
            var usename = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(usename);

            if(user == null || ! await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString()))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (DTO.UserRole.Equals(UserRole.CLIENT_REGULAR) || DTO.UserRole.Equals(UserRole.ADMIN))
                return BadRequest();

            try
            {
                var newUser = new User
                {
                    Email = DTO.Email.ToLower(),
                    UserName = DTO.Email.ToLower(),
                    FirstName = DTO.FirstName,
                    LastName = DTO.LastName,
                    UserRole = DTO.UserRole,
                    IsActive = true,
                    EmailConfirmed = true,
                    PhoneNumber = DTO.PhoneNumber,
                    PhoneNumberConfirmed = true
                };

                var createdUser = await _userManager.CreateAsync(newUser, DTO.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(newUser, newUser.UserRole.ToString());
                    if (roleResult.Succeeded)
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == DTO.username.ToLower());

            if (user == null)
                return Unauthorized("Invalid Credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, DTO.password, false);

            if(!result.Succeeded)
                return Unauthorized("Invalid Credentials");

            return Ok(
                new { 
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                });
        }
    }
}
