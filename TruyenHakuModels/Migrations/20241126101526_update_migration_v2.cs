using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruyenHakuModels.Migrations
{
    /// <inheritdoc />
    public partial class update_migration_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TotalChapter",
                table: "Manga");

            migrationBuilder.DropColumn(
                name: "CategoryEnum",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "TotalView",
                table: "Manga",
                newName: "TotalLikes");

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AnotherName",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Manga",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Manga");

            migrationBuilder.RenameColumn(
                name: "TotalLikes",
                table: "Manga",
                newName: "TotalView");

            migrationBuilder.AlterColumn<string>(
                name: "FolderPath",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnotherName",
                table: "Manga",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TotalChapter",
                table: "Manga",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CategoryEnum",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f4cf9d7-68b1-4436-ac91-99edcfc74735", "3", "Member", "Member" },
                    { "98b7d18c-d93a-4141-a24a-1752f6fac35c", "2", "Manager", "Manager" },
                    { "d77b9561-93ec-45ba-b296-0930f5f39f05", "1", "Admin", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CategoryEnum",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CategoryEnum",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CategoryEnum",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CategoryEnum",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CategoryEnum",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CategoryEnum",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CategoryEnum",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CategoryEnum",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CategoryEnum",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CategoryEnum",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CategoryEnum",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CategoryEnum",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CategoryEnum",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CategoryEnum",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CategoryEnum",
                value: 15);
        }
    }
}
