using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class ColumnsCountsInYears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColumnsCount",
                table: "Years",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnsCount",
                table: "Years");
        }
    }
}
