using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddPermsRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionRoles",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 2, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 3, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 4, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 5, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 6, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 7, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 8, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 9, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 10, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 11, "9365b6ea-c516-4174-a231-43c5975bb099" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 3, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 8, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, "9365b6ea-c516-4174-a231-43c5975bb099" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 11, "9365b6ea-c516-4174-a231-43c5975bb099" });
        }
    }
}
