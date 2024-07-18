using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebModels
{
    public class DataSeeding
    {
        public static void SeedData( ModelBuilder builder)
        {
            SeedRole(builder);  
        }

        private static void SeedRole( ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin",
            },
            new IdentityRole
            {
                Name = "Manager",
                ConcurrencyStamp = "2",
                NormalizedName = "Manager",
            },
            new IdentityRole
            {
                Name = "Member",
                ConcurrencyStamp = "3",
                NormalizedName = "Member",
            });
        }
    }
}
