using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class namefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FileUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FileUploads");
        }
    }
}
