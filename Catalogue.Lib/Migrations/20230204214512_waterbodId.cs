using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class waterbodId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenVisited",
                table: "WaterBodyDetectionDatas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWaterBodyPresent",
                table: "WaterBodyDetectionDatas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenVisited",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "IsWaterBodyPresent",
                table: "WaterBodyDetectionDatas");
        }
    }
}
