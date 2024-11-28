using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class createentityWebCssSelectorandseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MangaId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathDirectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Manga_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Manga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebCssSelector",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MangaNameSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnotherNameSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageThumbURLSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListChapterSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageSelectors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageAttribute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Https = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebCssSelector", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WebCssSelector",
                columns: new[] { "Id", "AnotherNameSelectors", "AuthorSelectors", "Https", "ImageAttribute", "ImageSelectors", "ImageThumbURLSelectors", "ListChapterSelectors", "MangaNameSelectors", "WebName" },
                values: new object[,]
                {
                    { 1L, ".other-name", ".author col-xs-8", "https://nettruyenviet.com", "data-src", ".reading-detail .page-chapter img", ".image-thumb", ".list-chapter > nav #desc li .chapter a", ".title-detail", "NetTruyenViet" },
                    { 2L, "", "", "https://truyenqq.com", "src", "#list_image .page-chapter img", ".book_avatar img", ".works-chapter-list .name-chap a", ".book_other h1", "TruyenQQ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_MangaId",
                table: "Chapter",
                column: "MangaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "WebCssSelector");
        }
    }
}
