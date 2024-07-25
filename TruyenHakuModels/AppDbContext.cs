using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TruyenHakuModels.Entities;

namespace TruyenHakuModels
{
    public class AppDbContext : IdentityDbContext<UserAccount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<UserAccount> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Manga> Manga { get; set; }
        //public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DataSeeding.SeedData(builder);
        }
    }
}
