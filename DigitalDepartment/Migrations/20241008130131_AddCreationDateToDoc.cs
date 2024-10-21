using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationDateToDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Documents");
        }
    }
}
