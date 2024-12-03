using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WebCssSelector",
                columns: new[] { "Id", "AnotherNameSelectors", "AuthorSelectors", "Https", "ImageAttribute", "ImageSelectors", "ImageThumbURLSelectors", "ListChapterSelectors", "MangaNameSelectors", "WebName" },
                values: new object[,]
                {
                    { 4L, "kokoko", "", "", "src", ".reading-content img", ".summary_image img", ".listing-chapters_wrap li a", ".post-title h1", "qManga" },
                    { 5L, "kokoko", "", "", "src", ".reading-content img", ".summary_image img", ".listing-chapters_wrap li a", ".post-title h1", "truyenvn" },
                    { 6L, "kokoko", "kokoko", "", "src", "#list_img img", ".summary_image img", ".works-chapter-list a", ".book_other h1", "truyenqq5" },
                    { 7L, "kokoko", "kokoko", "", "src", ".reading-detail img", ".col-image img", ".list-chapter nav li a", ".title-detail", "nettruyenqqviet" },
                    { 8L, "kokoko", "kokoko", "", "src", ".reading-detail img", "kokoko", ".chapter-table nav tr a", "kokoko", "khotruyen2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 8L);
        }
    }
}
