using Americans_Image_Capture.Shared.DTOs;
using Americans_Image_Capture.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Novell.Directory.Ldap; // Ensure this is installed
using Americans_Image_Capture.Server.Contracts;

namespace Americans_Image_Capture.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        //public AccountController(UserManager<AppUser> userManager,
        //                         SignInManager<AppUser> signInManager,
        //                         ITokenService tokenService)
        //{
        //    _signInManager = signInManager;
        //    _userManager = userManager;
        //    _tokenService = tokenService;
        //}

        public AccountController(ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }


        [Authorize]
        [HttpGet("validate")]
        public ActionResult Validate()
        {
            return Ok("Logged in");
        }
       
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                // Check if the user exists in the database
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Username.ToLower());
                if (user == null)
                {
                    return Unauthorized("Username or Password is invalid");
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [AllowAnonymous]
        [HttpPost("login-ldap")]
        public async Task<ActionResult> LoginLdap(LoginDto loginDto)
        {
            if (loginDto.Username.ToLower() == "test")
            {
                return await Login(loginDto);
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Username or Password is invalid");

            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect("172.25.128.10", LdapConnection.DefaultPort); // Synchronous Connect
                    connection.Bind(loginDto.Username, loginDto.Password);          // Synchronous Bind

                    if (connection.Bound)
                    {
                        var token = await _tokenService.CreateToken(user);
                        return Ok(token);
                    }
                }
            }
            catch (LdapException ex) when (ex.Message == "Invalid Credentials")
            {
                return Unauthorized("Username or Password Invalid");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }

            return Unauthorized("Username or Password Invalid");
        }

    }
}
