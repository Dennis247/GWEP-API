using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class TableFIleUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fileId",
                table: "WaterBodyDetectionDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropColumn(
                name: "fileId",
                table: "WaterBodyDetectionDatas");
        }
    }
}
