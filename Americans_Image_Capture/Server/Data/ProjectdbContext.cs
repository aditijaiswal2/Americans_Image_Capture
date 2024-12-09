
using Americans_Image_Capture.Shared.Entities;
using Americans_Image_Capture.Shared.Models;
using Americans_Image_Capture.Shared.Models.Maag_Americans_image;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Americans_Image_Capture.Server.Data
{
    public class ProjectdbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public ProjectdbContext(DbContextOptions<ProjectdbContext> options)
          : base(options)
        {
        }
        public DbSet<LoginUserDetails> LoginUserDetails { get; set; }
        public DbSet<Logindetail> Logindetails { get; set; }
        public DbSet<MaagAmericansImage> MaagAmericansImages { get; set; }
         public DbSet<Imagedata> Images { get; set; }


     
          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity tables to use int keys
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });

            modelBuilder.Entity<Imagedata>()
                .HasOne(i => i.MAAGImage)
                .WithMany(m => m.Images)
                .HasForeignKey(i => i.MAAGImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }



    }
}
