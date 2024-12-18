﻿using Microsoft.AspNetCore.Identity;
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
            SeedWebCssSelectors(builder);
        }

        private static void SeedRole(ModelBuilder builder)
        {
            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Name = "Admin",
            //    ConcurrencyStamp = "1",
            //    NormalizedName = "Admin",
            //},
            //new IdentityRole
            //{
            //    Name = "Manager",
            //    ConcurrencyStamp = "2",
            //    NormalizedName = "Manager",
            //},
            //new IdentityRole
            //{
            //    Name = "Member",
            //    ConcurrencyStamp = "3",
            //    NormalizedName = "Member",
            //});
        }

        private static void SeedCategory (ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action"
                },
                new Category
                {
                    Id = 2,
                    Name = "Adventure"
                },
                new Category
                {
                    Id = 3,
                    Name = "Isekai"
                },
                new Category
                {
                    Id = 4,
                    Name = "Fantasy"
                },
                new Category
                {
                    Id = 5,
                    Name = "Comedy"
                },
                new Category
                {
                    Id = 6,
                    Name = "Romance"
                },
                new Category
                {
                    Id = 7,
                    Name = "Psychological"
                },
                new Category
                {
                    Id = 8,
                    Name = "Supernatural"
                },
                new Category
                {
                    Id = 9,
                    Name = "Ecchi"
                },
                new Category
                {
                    Id = 10,
                    Name = "Shounen"
                },
                new Category
                {
                    Id = 11,
                    Name = "Seinen"
                },
                new Category
                {
                    Id = 12,
                    Name = "Soujo"
                },
                new Category
                {
                    Id = 13,
                    Name = "Yaoi"
                },
                new Category
                {
                    Id = 14,
                    Name = "Horror"
                },
                new Category
                {
                    Id = 15,
                    Name = "SliceOfLife"
                }
            );
        }

        private static void SeedWebCssSelectors(ModelBuilder builder)
        {
            builder.Entity<WebCssSelector>().HasData(
                new WebCssSelector
                {
                    Id = 1, // Hoặc sử dụng một ID cố định nếu cần
                    WebName = "NetTruyenViet",
                    MangaNameSelectors = ".title-detail",
                    AnotherNameSelectors = ".other-name",
                    AuthorSelectors = ".author col-xs-8",
                    ImageThumbURLSelectors = ".image-thumb",
                    ListChapterSelectors = ".list-chapter > nav #desc li .chapter a",
                    ImageSelectors = ".reading-detail .page-chapter img",
                    ImageAttribute = "data-src",
                    Https = "https://nettruyenviet.com"
                },
                new WebCssSelector
                {
                    Id = 2, // Hoặc sử dụng một ID cố định nếu cần
                    WebName = "TruyenQQ",
                    MangaNameSelectors = ".book_other h1",
                    AnotherNameSelectors = "",
                    AuthorSelectors = "",
                    ImageThumbURLSelectors = ".book_avatar img",
                    ListChapterSelectors = ".works-chapter-list .name-chap a",
                    ImageSelectors = "#list_image .page-chapter img",
                    ImageAttribute = "src",
                    Https = "https://truyenqq.com"
                }
            );
        }


    }
}
