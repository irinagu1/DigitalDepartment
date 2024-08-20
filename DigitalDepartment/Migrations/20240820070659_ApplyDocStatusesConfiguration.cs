using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class ApplyDocStatusesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DocumentStatuses",
                columns: new[] { "DocumentStatusId", "Name", "isEnable" },
                values: new object[,]
                {
                    { 1, "New", true },
                    { 2, "In process", true },
                    { 3, "Finished", true },
                    { 4, "Closed", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentStatuses",
                keyColumn: "DocumentStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DocumentStatuses",
                keyColumn: "DocumentStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocumentStatuses",
                keyColumn: "DocumentStatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DocumentStatuses",
                keyColumn: "DocumentStatusId",
                keyValue: 4);
        }
    }
}
