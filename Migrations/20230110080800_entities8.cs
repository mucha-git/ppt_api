using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_MapPins_PinId",
                table: "Markers");

            migrationBuilder.DropIndex(
                name: "IX_Markers_PinId",
                table: "Markers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Markers_PinId",
                table: "Markers",
                column: "PinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_MapPins_PinId",
                table: "Markers",
                column: "PinId",
                principalTable: "MapPins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
