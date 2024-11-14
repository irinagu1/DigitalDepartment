using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalDepartment.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToChecks_Documents_DocumentId",
                table: "ToChecks");

            migrationBuilder.DropIndex(
                name: "IX_ToChecks_DocumentId",
                table: "ToChecks");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "ToChecks");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Documents");

            migrationBuilder.AddColumn<long>(
                name: "VersionId",
                table: "ToChecks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentVersions",
                columns: table => new
                {
                    DocumentVersionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    isLast = table.Column<bool>(type: "bit", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentVersions", x => x.DocumentVersionId);
                    table.ForeignKey(
                        name: "FK_DocumentVersions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToChecks_VersionId",
                table: "ToChecks",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersions_DocumentId",
                table: "DocumentVersions",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToChecks_DocumentVersions_VersionId",
                table: "ToChecks",
                column: "VersionId",
                principalTable: "DocumentVersions",
                principalColumn: "DocumentVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToChecks_DocumentVersions_VersionId",
                table: "ToChecks");

            migrationBuilder.DropTable(
                name: "DocumentVersions");

            migrationBuilder.DropIndex(
                name: "IX_ToChecks_VersionId",
                table: "ToChecks");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "ToChecks");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "ToChecks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ToChecks_DocumentId",
                table: "ToChecks",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToChecks_Documents_DocumentId",
                table: "ToChecks",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId");
        }
    }
}
