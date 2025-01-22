using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PositionId", "RefreshToken", "RefreshTokenExpiryTime", "SecondName", "SecurityStamp", "TwoFactorEnabled", "UserName", "isActive" },
                values: new object[] { "9f8f4248-953c-409b-a048-ac08324f19fe", 0, "b9810a7a-54b2-4e7f-a8c0-f5e1e78fb810", "superadming@gmail.com", false, "Full", "Administrator", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAECaFM/EyVqmzR0nPT9SFF6qvDJkp2rURb83BDmBcrM/Lb0ya3JZtbNxZVOpflyWy0w==", "12345", false, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Access", "c986b1be-0df6-4d39-ac71-94fa6511e4a6", false, "superadmin", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f8f4248-953c-409b-a048-ac08324f19fe");
        }
    }
}
