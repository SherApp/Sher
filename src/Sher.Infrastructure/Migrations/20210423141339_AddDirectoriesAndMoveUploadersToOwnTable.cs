using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sher.Infrastructure.Migrations
{
    public partial class AddDirectoriesAndMoveUploadersToOwnTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UploaderId",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectoryId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Uploaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uploaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uploaders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentDirectoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    UploaderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directory_Directory_ParentDirectoryId",
                        column: x => x.ParentDirectoryId,
                        principalTable: "Directory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Directory_Uploaders_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Uploaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directory_Name",
                table: "Directory",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Directory_ParentDirectoryId",
                table: "Directory",
                column: "ParentDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Directory_UploaderId",
                table: "Directory",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Uploaders_UserId",
                table: "Uploaders",
                column: "UserId");
            
            migrationBuilder.Sql("INSERT INTO \"Uploaders\" (\"Id\", \"UserId\") SELECT uuid_generate_v4(), \"Id\" FROM \"Users\"");
            
            migrationBuilder.Sql(
                "UPDATE \"Files\" SET \"UploaderId\" = \"Uploaders\".\"Id\" FROM \"Uploaders\" WHERE \"Files\".\"UploaderId\" = \"Uploaders\".\"UserId\"");

            migrationBuilder.Sql(
                "INSERT INTO \"Directory\" SELECT uuid_generate_v4(), NULL, \"Id\", 'Root' FROM \"Uploaders\"");
                
            migrationBuilder.Sql(
                "UPDATE \"Files\" SET \"DirectoryId\" = \"Directory\".\"Id\" FROM \"Directory\" WHERE \"Directory\".\"UploaderId\" = \"Files\".\"UploaderId\"");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Uploaders_UploaderId",
                table: "Files",
                column: "UploaderId",
                principalTable: "Uploaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Uploaders_UploaderId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Directory");

            migrationBuilder.DropTable(
                name: "Uploaders");

            migrationBuilder.DropColumn(
                name: "DirectoryId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UploaderId",
                table: "Files",
                column: "UploaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
