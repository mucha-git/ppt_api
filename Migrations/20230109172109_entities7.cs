using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Polylines",
                table: "Maps",
                type: "BLOB SUB_TYPE TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Polylines",
                table: "Maps");

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
    }
}
