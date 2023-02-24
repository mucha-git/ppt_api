using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Maps_MapId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_MapId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Elements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "Elements",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_MapId",
                table: "Elements",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Maps_MapId",
                table: "Elements",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id");
        }
    }
}
