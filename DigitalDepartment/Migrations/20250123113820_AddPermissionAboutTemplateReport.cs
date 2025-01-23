using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionAboutTemplateReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "843a53db-b8a3-40fc-92df-f8cc47b18de0", "c6f693ed-2cd3-42f7-9a50-f7b9ba25143b" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Category", "Name" },
                values: new object[] { 12, "Шаблон отчета", "Редактирование шаблона отчета" });

            migrationBuilder.InsertData(
                table: "PermissionRoles",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 12, "9365b6ea-c516-4174-a231-43c5975bb099" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 12, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "051f6ef9-eddb-47a5-929b-1a5be0796f53", "79b12de4-dc5f-4337-8a64-61c5c3c3ce53" });
        }
    }
}
