using Americans_Image_Capture.Server.Contracts;
using Americans_Image_Capture.Server.Data;
using Americans_Image_Capture.Shared.DTOs;
using Americans_Image_Capture.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Americans_Image_Capture.Server.Services
{
    public class LoginUserDetailsService : ILoginUserDetails
    {
        private readonly ProjectdbContext _context;

        public LoginUserDetailsService(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<LoginUserDetailsDTO> CreateLoginUserDetailsAsync(LoginUserDetailsDTO loginUserDetailsDto)
        {            
            var userDetails = new LoginUserDetails
            {
                UserName = loginUserDetailsDto.UserName,
                EMail = loginUserDetailsDto.EMail,
                LoginDateAndTime = loginUserDetailsDto.LoginDateAndTime
            };

            _context.LoginUserDetails.Add(userDetails);
            await _context.SaveChangesAsync();

            return loginUserDetailsDto;
        }

        public async Task<IEnumerable<LoginUserDetailsDTO>> GetAllLoginUserDetailsAsync()
        {
            var userDetails = await _context.LoginUserDetails.ToListAsync();
            return userDetails.Select(user => new LoginUserDetailsDTO
            {
                UserName = user.UserName,
                EMail = user.EMail,
                LoginDateAndTime = user.LoginDateAndTime
            }).ToList();
        }
    }
}
