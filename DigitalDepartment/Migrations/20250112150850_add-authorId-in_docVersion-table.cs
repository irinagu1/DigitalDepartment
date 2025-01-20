using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class addauthorIdin_docVersiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "DocumentVersions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersions_AuthorId",
                table: "DocumentVersions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_AspNetUsers_AuthorId",
                table: "DocumentVersions",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_AspNetUsers_AuthorId",
                table: "DocumentVersions");

            migrationBuilder.DropIndex(
                name: "IX_DocumentVersions_AuthorId",
                table: "DocumentVersions");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "DocumentVersions");
        }
    }
}
