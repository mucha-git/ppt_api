using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RemovePropsFromElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autoplay",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "DestinationViewId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "ImgSrc",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "MapHeight",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Margin",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Playlist",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Elements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Autoplay",
                table: "Elements",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationViewId",
                table: "Elements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Elements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgSrc",
                table: "Elements",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MapHeight",
                table: "Elements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Margin",
                table: "Elements",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Playlist",
                table: "Elements",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Elements",
                type: "text",
                nullable: true);
        }
    }
}
