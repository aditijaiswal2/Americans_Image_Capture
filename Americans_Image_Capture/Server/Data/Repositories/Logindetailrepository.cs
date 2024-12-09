using Americans_Image_Capture.Server.Contracts;
using Americans_Image_Capture.Shared.Models.Maag_Americans_image;
using Microsoft.EntityFrameworkCore;
using System;

namespace Americans_Image_Capture.Server.Data.Repositories
{
    public class Logindetailrepository : ILogindetailrepository
    {
        private readonly ProjectdbContext _context;

        public Logindetailrepository(ProjectdbContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(Logindetail user)
        {
            _context.Logindetails.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Logindetail>> GetAllUsersAsync()
        {
            return await _context.Logindetails.ToListAsync();
        }

        public async Task<Logindetail> GetUserByIdAsync(int userId)
        {
            return await _context.Logindetails.FirstOrDefaultAsync(u => u.UserId == userId); 
        }
    }
}
