using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "varchar(200)", nullable: true),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: true),
                    FooterText = table.Column<string>(type: "varchar(100)", nullable: true),
                    FooterColor = table.Column<string>(type: "varchar(10)", nullable: true),
                    StrokeWidth = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    PinId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilgrimages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(500)", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    LogoSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    OneSignal = table.Column<string>(type: "varchar(50)", nullable: true),
                    OneSignalApiKey = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilgrimages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "varchar(80)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(80)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(80)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(1000)", nullable: true),
                    AcceptTerms = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    VerificationToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Verified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ResetToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordReset = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PilgrimageId = table.Column<int>(type: "integer", nullable: true),
                    Random = table.Column<string>(type: "varchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Pilgrimages_PilgrimageId",
                        column: x => x.PilgrimageId,
                        principalTable: "Pilgrimages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    YearTopic = table.Column<string>(type: "varchar(500)", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Version = table.Column<Guid>(type: "uuid", nullable: true),
                    PilgrimageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Years_Pilgrimages_PilgrimageId",
                        column: x => x.PilgrimageId,
                        principalTable: "Pilgrimages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(80)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "varchar(80)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "varchar(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapPins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(250)", nullable: true),
                    PinSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IconSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    YearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapPins_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Provider = table.Column<string>(type: "varchar(50)", nullable: true),
                    Name = table.Column<string>(type: "varchar(250)", nullable: true),
                    MapSrc = table.Column<string>(type: "text", nullable: true),
                    StrokeColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    StrokeWidth = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Delta = table.Column<double>(type: "double precision", nullable: true),
                    MarkersString = table.Column<string>(type: "text", nullable: true),
                    Polylines = table.Column<string>(type: "text", nullable: true),
                    YearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maps_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "varchar(80)", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    HeaderText = table.Column<string>(type: "varchar(80)", nullable: true),
                    ScreenType = table.Column<int>(type: "integer", nullable: true),
                    ExternalUrl = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: true),
                    YearId = table.Column<int>(type: "integer", nullable: false),
                    ViewId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Views_ViewId",
                        column: x => x.ViewId,
                        principalTable: "Views",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Views_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", nullable: true),
                    Margin = table.Column<int>(type: "integer", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Text = table.Column<string>(type: "text", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Autoplay = table.Column<bool>(type: "boolean", nullable: true),
                    Playlist = table.Column<string>(type: "varchar(1000)", nullable: true),
                    mapHeight = table.Column<int>(type: "integer", nullable: true),
                    MapId = table.Column<int>(type: "integer", nullable: true),
                    DestinationViewId = table.Column<int>(type: "integer", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: true),
                    ViewId = table.Column<int>(type: "integer", nullable: false),
                    YearId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Elements_Views_ViewId",
                        column: x => x.ViewId,
                        principalTable: "Views",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Elements_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PilgrimageId",
                table: "Accounts",
                column: "PilgrimageId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_MapId",
                table: "Elements",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_ViewId",
                table: "Elements",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_YearId",
                table: "Elements",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_MapPins_YearId",
                table: "MapPins",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_YearId",
                table: "Maps",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_ViewId",
                table: "Views",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_YearId",
                table: "Views",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_Years_PilgrimageId",
                table: "Years",
                column: "PilgrimageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "MapPins");

            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Pilgrimages");
        }
    }
}
