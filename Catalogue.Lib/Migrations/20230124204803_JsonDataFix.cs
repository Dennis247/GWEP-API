using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class JsonDataFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonData",
                table: "WaterBodyDetectionDatas",
                newName: "type");

            migrationBuilder.AddColumn<string>(
                name: "crs",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "featureGometry",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "featureProperties",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "featureType",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "WaterBodyDetectionDatas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "crs",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "featureGometry",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "featureProperties",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "featureType",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "name",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "WaterBodyDetectionDatas",
                newName: "JsonData");
        }
    }
}
