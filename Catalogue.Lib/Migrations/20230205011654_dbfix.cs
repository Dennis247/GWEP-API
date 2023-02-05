using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class dbfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "WaterBodyDetectionDatas");
        }
    }
}
