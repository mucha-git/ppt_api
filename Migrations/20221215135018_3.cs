using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Autoplay",
                table: "Elements",
                type: "BOOLEAN",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Autoplay",
                table: "Elements",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN",
                oldNullable: true);
        }
    }
}
