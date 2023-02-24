using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Maps_MapId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_MapId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Coordinates");

            migrationBuilder.AddColumn<int>(
                name: "MapsId",
                table: "Coordinates",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_MapsId",
                table: "Coordinates",
                column: "MapsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Maps_MapsId",
                table: "Coordinates",
                column: "MapsId",
                principalTable: "Maps",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Maps_MapsId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_MapsId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "MapsId",
                table: "Coordinates");

            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "Coordinates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_MapId",
                table: "Coordinates",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Maps_MapId",
                table: "Coordinates",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
