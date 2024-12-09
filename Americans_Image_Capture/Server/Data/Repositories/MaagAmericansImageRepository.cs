using Americans_Image_Capture.Server.Contracts;

using Microsoft.EntityFrameworkCore;
using System;

namespace Americans_Image_Capture.Server.Data.Repositories
{
    public class MaagAmericansImageRepository : IMaagAmericansImageRepository
    {
        private readonly ProjectdbContext _context;
        public MaagAmericansImageRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaagAmericansImage>> GetAllAsync()
        {
            return await _context.MaagAmericansImages.Include(m => m.Images).ToListAsync();
        }

        public async Task<MaagAmericansImage> GetByIdAsync(int id)
        {
            return await _context.MaagAmericansImages
                .Include(m => m.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(MaagAmericansImage image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            // Ensure the foreign key relationship is correctly established
            foreach (var imageData in image.Images)
            {
                imageData.MAAGImage = image; // Link each Imagedata object to the parent MaagAmericansImage
            }

            // Add the MaagAmericansImage object to the DbContext
            await _context.MaagAmericansImages.AddAsync(image);

            // Save changes to persist the data
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(MaagAmericansImage image)
        {
            _context.MaagAmericansImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _context.MaagAmericansImages.FindAsync(id);
            if (image != null)
            {
                _context.MaagAmericansImages.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
