using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class latitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lalitude",
                table: "Markers",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "Lalitude",
                table: "Maps",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "Lalitude",
                table: "Coordinates",
                newName: "Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Markers",
                newName: "Lalitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Maps",
                newName: "Lalitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Coordinates",
                newName: "Lalitude");
        }
    }
}
