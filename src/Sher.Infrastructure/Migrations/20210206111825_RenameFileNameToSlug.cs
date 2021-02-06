using Microsoft.EntityFrameworkCore.Migrations;

namespace Sher.Infrastructure.Migrations
{
    public partial class RenameFileNameToSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Files",
                newName: "FileName");
        }
    }
}
