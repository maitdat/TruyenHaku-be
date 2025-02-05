using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NgaySinh",
                table: "AspNetUsers",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "HoTen",
                table: "AspNetUsers",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "HoTen");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "AspNetUsers",
                newName: "NgaySinh");
        }
    }
}
