using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "9365b6ea-c516-4174-a231-43c5975bb099", "9f8f4248-953c-409b-a048-ac08324f19fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "051f6ef9-eddb-47a5-929b-1a5be0796f53", "79b12de4-dc5f-4337-8a64-61c5c3c3ce53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9365b6ea-c516-4174-a231-43c5975bb099", "9f8f4248-953c-409b-a048-ac08324f19fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b9810a7a-54b2-4e7f-a8c0-f5e1e78fb810", "c986b1be-0df6-4d39-ac71-94fa6511e4a6" });
        }
    }
}
