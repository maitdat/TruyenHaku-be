using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manga_Author_AuthorId",
                table: "Manga");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ddb4ce4-e678-4466-b222-eba16ff808d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ec656bf-6be4-4dd7-b092-e6c858c6298a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1d24647-96fb-4b86-a930-92aca4cd6b30");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Manga");

            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "Manga",
                newName: "FolderPath");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Manga",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Manga",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Manga",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Manga",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "LastChapter",
                table: "Manga",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ModifierName",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalChapter",
                table: "Manga",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MangaCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    MangaId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaCategory_Manga_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Manga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f4cf9d7-68b1-4436-ac91-99edcfc74735", "3", "Member", "Member" },
                    { "98b7d18c-d93a-4141-a24a-1752f6fac35c", "2", "Manager", "Manager" },
                    { "d77b9561-93ec-45ba-b296-0930f5f39f05", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MangaCategory_CategoryId",
                table: "MangaCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaCategory_MangaId",
                table: "MangaCategory",
                column: "MangaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manga_Author_AuthorId",
                table: "Manga",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manga_Author_AuthorId",
                table: "Manga");

            migrationBuilder.DropTable(
                name: "MangaCategory");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f4cf9d7-68b1-4436-ac91-99edcfc74735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98b7d18c-d93a-4141-a24a-1752f6fac35c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d77b9561-93ec-45ba-b296-0930f5f39f05");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "LastChapter",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "ModifierName",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "TotalChapter",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "FolderPath",
                table: "Manga",
                newName: "Chapter");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Manga",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ddb4ce4-e678-4466-b222-eba16ff808d3", "3", "Member", "Member" },
                    { "8ec656bf-6be4-4dd7-b092-e6c858c6298a", "2", "Manager", "Manager" },
                    { "e1d24647-96fb-4b86-a930-92aca4cd6b30", "1", "Admin", "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Manga_Author_AuthorId",
                table: "Manga",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
