using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TruyenHakuCommon;
using TruyenHakuModels.Entities;

namespace TruyenHakuModels
{
    public class DataSeeding
    {
        public static void SeedData(ModelBuilder builder)
        {
            SeedRole(builder);
            SeedCategory(builder);
        }

        private static void SeedRole(ModelBuilder builder)
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

        private static void SeedCategory (ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
            new Category
            {
                Name = "Action",
                CategoryEnum = Enums.Category.Action,
            },
            new Category
            {
                Name = "Adventure",
                CategoryEnum = Enums.Category.Adventure,
            },
            new Category
            {
                Name = "Isekai",
                CategoryEnum = Enums.Category.Isekai,
            },
            new Category
            {
                Name = "Fantasy",
                CategoryEnum = Enums.Category.Fantasy,
            },
            new Category
            {
                Name = "Comedy",
                CategoryEnum = Enums.Category.Comedy,
            },
            new Category
            {
                Name = "Romance",
                CategoryEnum = Enums.Category.Romance,
            },
            new Category
            {
                Name = "Pyschological",
                CategoryEnum = Enums.Category.Pyschological,
            },
            new Category
            {
                Name = "Supernatural",
                CategoryEnum = Enums.Category.Supernatural,
            },
            new Category
            {
                Name = "Ecchi",
                CategoryEnum = Enums.Category.Ecchi,
            },
            new Category
            {
                Name = "Shounen",
                CategoryEnum = Enums.Category.Shounen,
            },
            new Category
            {
                Name = "Seinen",
                CategoryEnum = Enums.Category.Seinen,
            },
            new Category
            {
                Name = "Soujo",
                CategoryEnum = Enums.Category.Soujo,
            },
            new Category
            {
                Name = "Yaoi",
                CategoryEnum = Enums.Category.Yaoi,
            },
            new Category
            {
                Name = "Horror",
                CategoryEnum = Enums.Category.Horror,
            },
            new Category
            {
                Name = "SliceOfLife",
                CategoryEnum = Enums.Category.SliceOfLife,
            });
        }
    }
}
