using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class clearPermsAndpermsRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, "483d51a8-37f5-473c-a17a-0b0d175c1e7e" });

            migrationBuilder.DeleteData(
                table: "PermissionRoles",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, "483d51a8-37f5-473c-a17a-0b0d175c1e7e" });

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
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "Category", "Name" },
                values: new object[,]
                {
                    { 1, "Пользователи", "Просмотр пользователей" },
                    { 2, "Пользователи", "Добавление пользователей" },
                    { 3, "Пользователи", "Редактирование пользователей" },
                    { 4, "Пользователи", "Архивирование пользователей" },
                    { 5, "Пользователи", "Удаление пользователей" }
                });

            migrationBuilder.InsertData(
                table: "PermissionRoles",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, "483d51a8-37f5-473c-a17a-0b0d175c1e7e" },
                    { 2, "483d51a8-37f5-473c-a17a-0b0d175c1e7e" },
                    { 1, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 2, "9365b6ea-c516-4174-a231-43c5975bb099" },
                    { 3, "9365b6ea-c516-4174-a231-43c5975bb099" }
                });
        }
    }
}
