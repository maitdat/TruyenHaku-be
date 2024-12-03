using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderPath",
                table: "Manga");

            migrationBuilder.RenameColumn(
                name: "PathDirectory",
                table: "Chapter",
                newName: "NameFolder");

            migrationBuilder.AddColumn<string>(
                name: "NameFolder",
                table: "Manga",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameFolder",
                table: "Manga");

            migrationBuilder.RenameColumn(
                name: "NameFolder",
                table: "Chapter",
                newName: "PathDirectory");

            migrationBuilder.AddColumn<string>(
                name: "FolderPath",
                table: "Manga",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
