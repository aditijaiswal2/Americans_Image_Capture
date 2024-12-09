using Americans_Image_Capture.Server.Data;
using Americans_Image_Capture.Shared.DTOs;
using Americans_Image_Capture.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Charts;

namespace Americans_Image_Capture.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        /**
         * Gets a list of users for Admin
         *
         * @return json
         **/
        //[Authorize(Roles = "Admin")]
        [HttpGet("getuser")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Where(u => u.isDeleted == 0)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Usercode = u.UserCode,
                    Name = u.Name,
                    Email = u.Email,
                    //PageNames = u.PageNames,
                    //Routes = u.Routes,
                })
                .ToListAsync();

            return Ok(users);
        }




        /**
         * Gets the user details
         *
         * @param string username
         * @return json
         **/
        // [Authorize(Roles = "Admin")]
        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Name = u.Name,
                    Email = u.Email,
                    //PageNames = u.PageNames,
                    //Routes = u.Routes,
                })
                .SingleOrDefaultAsync(u => u.Username == username.ToLower());

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }


        /**
         * Add a new user from Admin
         *
         * @param UserAddDto addDto
         * @return string
         **/
        //[Authorize(Roles = "Admin")]
        [HttpPost("adduser")]
        public async Task<ActionResult> AddUser(UserAddDto addDto)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == addDto.username.ToLower());

            if (existingUser != null)
            {
                if (existingUser.isDeleted == 1)
                {
                    existingUser.UserCode = addDto.Usercode.ToLower();
                    existingUser.Email = addDto.Email.ToLower();
                    existingUser.Name = addDto.Name;
                    existingUser.isDeleted = 0;
                  //  existingUser.PageNames = addDto.PageNames;
                 

                    _context.Entry(existingUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok("User restored successfully.");
                }
                else
                {
                    return BadRequest("Username is already taken, change the UserName.");
                }
            }
            else
            {
                var user = new AppUser
                {
                    UserName = addDto.username.ToLower(),
                    UserCode = addDto.Usercode.ToLower(),
                    Email = addDto.Email.ToLower(),
                    Name = addDto.Name,
                    //PageNames = addDto.PageNames,
                    //Routes = addDto.Routes
                };

                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _context.SaveChangesAsync();
                return Created("User created successfully", "");
            }
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Username is required.");

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username.ToLower());

            if (user == null)
                return NotFound("User does not exist.");

            user.isDeleted = 1; // Mark the user as deleted.

            _context.Entry(user).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return Ok(new { Message = $"User '{username}' deleted successfully." });

            return StatusCode(500, "Failed to delete user. Please try again.");
        }


    }
}