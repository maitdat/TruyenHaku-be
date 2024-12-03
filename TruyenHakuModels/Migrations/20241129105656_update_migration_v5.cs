using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WebCssSelector",
                columns: new[] { "Id", "AnotherNameSelectors", "AuthorSelectors", "Https", "ImageAttribute", "ImageSelectors", "ImageThumbURLSelectors", "ListChapterSelectors", "MangaNameSelectors", "WebName" },
                values: new object[] { 9L, "kokoko", "kokoko", "", "src", ".reading-detail img", "kokoko", ".list-chapter nav li a", ".title-detail", "nhattruyenss" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WebCssSelector",
                keyColumn: "Id",
                keyValue: 9L);
        }
    }
}
