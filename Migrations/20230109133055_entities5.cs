using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Views_Years_YearsId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Views_YearsId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "YearsId",
                table: "Views");

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Views",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Views_YearId",
                table: "Views",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Years_YearId",
                table: "Views",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Views_Years_YearId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Views_YearId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Views");

            migrationBuilder.AddColumn<int>(
                name: "YearsId",
                table: "Views",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Views_YearsId",
                table: "Views",
                column: "YearsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Years_YearsId",
                table: "Views",
                column: "YearsId",
                principalTable: "Years",
                principalColumn: "Id");
        }
    }
}
