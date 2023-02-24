using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Maps_MapId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Years_YearId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Views_YearId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Markers_MapId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Markers");

            migrationBuilder.AddColumn<int>(
                name: "YearsId",
                table: "Views",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MapsId",
                table: "Markers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Views_YearsId",
                table: "Views",
                column: "YearsId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_MapsId",
                table: "Markers",
                column: "MapsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Maps_MapsId",
                table: "Markers",
                column: "MapsId",
                principalTable: "Maps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Years_YearsId",
                table: "Views",
                column: "YearsId",
                principalTable: "Years",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markers_Maps_MapsId",
                table: "Markers");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Years_YearsId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Views_YearsId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Markers_MapsId",
                table: "Markers");

            migrationBuilder.DropColumn(
                name: "YearsId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "MapsId",
                table: "Markers");

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Views",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "Markers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Views_YearId",
                table: "Views",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_MapId",
                table: "Markers",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markers_Maps_MapId",
                table: "Markers",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Years_YearId",
                table: "Views",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
