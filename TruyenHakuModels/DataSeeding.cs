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
                    Id = 1,
                    Name = "Action",
                    CategoryEnum = Enums.Category.Action,
                },
                new Category
                {
                    Id = 2,
                    Name = "Adventure",
                    CategoryEnum = Enums.Category.Adventure,
                },
                new Category
                {
                    Id = 3,
                    Name = "Isekai",
                    CategoryEnum = Enums.Category.Isekai,
                },
                new Category
                {
                    Id = 4,
                    Name = "Fantasy",
                    CategoryEnum = Enums.Category.Fantasy,
                },
                new Category
                {
                    Id = 5,
                    Name = "Comedy",
                    CategoryEnum = Enums.Category.Comedy,
                },
                new Category
                {
                    Id = 6,
                    Name = "Romance",
                    CategoryEnum = Enums.Category.Romance,
                },
                new Category
                {
                    Id = 7,
                    Name = "Psychological",
                    CategoryEnum = Enums.Category.Psychological,
                },
                new Category
                {
                    Id = 8,
                    Name = "Supernatural",
                    CategoryEnum = Enums.Category.Supernatural,
                },
                new Category
                {
                    Id = 9,
                    Name = "Ecchi",
                    CategoryEnum = Enums.Category.Ecchi,
                },
                new Category
                {
                    Id = 10,
                    Name = "Shounen",
                    CategoryEnum = Enums.Category.Shounen,
                },
                new Category
                {
                    Id = 11,
                    Name = "Seinen",
                    CategoryEnum = Enums.Category.Seinen,
                },
                new Category
                {
                    Id = 12,
                    Name = "Soujo",
                    CategoryEnum = Enums.Category.Soujo,
                },
                new Category
                {
                    Id = 13,
                    Name = "Yaoi",
                    CategoryEnum = Enums.Category.Yaoi,
                },
                new Category
                {
                    Id = 14,
                    Name = "Horror",
                    CategoryEnum = Enums.Category.Horror,
                },
                new Category
                {
                    Id = 15,
                    Name = "SliceOfLife",
                    CategoryEnum = Enums.Category.SliceOfLife,
                }
            );
        }
    }
}
