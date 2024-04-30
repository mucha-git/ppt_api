using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class GpsDataInMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GpsNavigationColor",
                table: "Maps",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GpsNavigationText",
                table: "Maps",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GpsTitle",
                table: "Maps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GpsNavigationColor",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "GpsNavigationText",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "GpsTitle",
                table: "Maps");
        }
    }
}
