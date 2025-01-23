using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class Change6PermissionOnSeeAboutYourself : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b989b76f-4edc-4373-a760-75d93eb217a5", "11348704-a015-46a7-9cf6-dd9d625f7e64" });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6,
                columns: new[] { "Category", "Name" },
                values: new object[] { "О себе", "Просмотр информации о себе" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "843a53db-b8a3-40fc-92df-f8cc47b18de0", "c6f693ed-2cd3-42f7-9a50-f7b9ba25143b" });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6,
                columns: new[] { "Category", "Name" },
                values: new object[] { "Документы", "Просмотр своих документов" });
        }
    }
}
