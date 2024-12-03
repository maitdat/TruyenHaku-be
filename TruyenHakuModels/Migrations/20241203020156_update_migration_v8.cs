using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Chapter");
        }
    }
}
