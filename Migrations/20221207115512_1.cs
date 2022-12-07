using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(80)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(80)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(80)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(1000)", nullable: true),
                    AcceptTerms = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    VerificationToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Verified = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    ResetToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    PasswordReset = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Expires = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(80)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    RevokedByIp = table.Column<string>(type: "varchar(80)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "varchar(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Accounts_Accou~",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
