using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class Letter_Document_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "DocumentCategories",
                columns: new[] { "DocumentCategoryId", "Name", "isEnable" },
                values: new object[] { 3, "ThirdCategory", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentCategories",
                keyColumn: "DocumentCategoryId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Documents");
        }
    }
}
