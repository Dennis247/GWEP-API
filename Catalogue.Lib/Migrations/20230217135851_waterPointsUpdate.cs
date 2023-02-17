using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class waterPointsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTimeUpdated",
                table: "WaterBodyDetectionDatas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedBy",
                table: "WaterBodyDetectionDatas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataSyncs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyncedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCount = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSyncs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterBodyPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OBJECTID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UNIQUE_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHASE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CONFIDENCE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AREA_SQM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SHAPE_Leng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SHAPE_Area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWaterBodyPresent = table.Column<bool>(type: "bit", nullable: false),
                    HasBeenVisited = table.Column<bool>(type: "bit", nullable: false),
                    LastTimeVisisted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastVisistedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterBodyPoints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSyncs");

            migrationBuilder.DropTable(
                name: "WaterBodyPoints");

            migrationBuilder.DropColumn(
                name: "LastTimeUpdated",
                table: "WaterBodyDetectionDatas");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "WaterBodyDetectionDatas");
        }
    }
}
