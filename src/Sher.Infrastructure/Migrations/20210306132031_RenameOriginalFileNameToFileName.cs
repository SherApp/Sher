using Microsoft.EntityFrameworkCore.Migrations;

namespace Sher.Infrastructure.Migrations
{
    public partial class RenameOriginalFileNameToFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalFileName",
                table: "Files",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "OriginalFileName");
        }
    }
}
