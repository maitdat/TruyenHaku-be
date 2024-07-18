using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebModels.Entities;

namespace WebModels
{
    public class AppDbContext : IdentityDbContext<UserAccount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<UserAccount> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DataSeeding.SeedData(builder);
        }
    }
}
