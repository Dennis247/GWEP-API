using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogue.Lib.Migrations
{
    public partial class dbupgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbatePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbateCaptain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Woreda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kebele = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillageSharingWaterSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaterSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    TypeofWaterSource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reasonsforusingwatersource = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbatePoints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbatePoints");
        }
    }
}
