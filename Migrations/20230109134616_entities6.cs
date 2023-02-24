using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class entities6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Years_YearsId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_YearsId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "YearsId",
                table: "Elements");

            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_YearId",
                table: "Elements",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Years_YearId",
                table: "Elements",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Years_YearId",
                table: "Elements");

            migrationBuilder.DropIndex(
                name: "IX_Elements_YearId",
                table: "Elements");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Elements");

            migrationBuilder.AddColumn<int>(
                name: "YearsId",
                table: "Elements",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elements_YearsId",
                table: "Elements",
                column: "YearsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Years_YearsId",
                table: "Elements",
                column: "YearsId",
                principalTable: "Years",
                principalColumn: "Id");
        }
    }
}
