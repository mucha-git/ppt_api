using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class changeMapSrc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapSrc",
                table: "Elements");

            migrationBuilder.AddColumn<string>(
                name: "MapSrc",
                table: "Maps",
                type: "BLOB SUB_TYPE TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapSrc",
                table: "Maps");

            migrationBuilder.AddColumn<string>(
                name: "MapSrc",
                table: "Elements",
                type: "varchar(500)",
                nullable: true);
        }
    }
}
