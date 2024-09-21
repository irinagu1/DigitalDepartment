using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorIdInLetterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Letters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Letters_AuthorId",
                table: "Letters",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Letters_AspNetUsers_AuthorId",
                table: "Letters",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Letters_AspNetUsers_AuthorId",
                table: "Letters");

            migrationBuilder.DropIndex(
                name: "IX_Letters_AuthorId",
                table: "Letters");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Letters");
        }
    }
}
