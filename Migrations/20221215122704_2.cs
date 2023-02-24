using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PilgrimageId",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pilgrimages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(500)", nullable: true),
                    isActive = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    LogoSrc = table.Column<string>(type: "varchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilgrimages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    YearTopic = table.Column<string>(type: "varchar(500)", nullable: true),
                    isActive = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    PilgrimageId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Years_Pilgrimages_Pilgrimag~",
                        column: x => x.PilgrimageId,
                        principalTable: "Pilgrimages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapPins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    PinSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IconSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    YearId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(type: "varchar(50)", nullable: true),
                    StrokeColor = table.Column<string>(type: "varchar(50)", nullable: true),
                    StrokeWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    Lalitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    Longitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    Delta = table.Column<double>(type: "DOUBLE PRECISION", nullable: true),
                    YearId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(80)", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    HeaderText = table.Column<string>(type: "varchar(80)", nullable: true),
                    ScreenType = table.Column<int>(type: "INTEGER", nullable: true),
                    ExternalUrl = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    YearId = table.Column<int>(type: "INTEGER", nullable: false),
                    ViewId = table.Column<int>(type: "INTEGER", nullable: true)
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
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Lalitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    Longitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    MapId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinates_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    FooterText = table.Column<string>(type: "varchar(50)", nullable: true),
                    FooterColor = table.Column<string>(type: "varchar(10)", nullable: true),
                    StrokeWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    Lalitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    Longitude = table.Column<double>(type: "DOUBLE PRECISION", nullable: false),
                    PinId = table.Column<int>(type: "INTEGER", nullable: false),
                    MapId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markers_MapPins_PinId",
                        column: x => x.PinId,
                        principalTable: "MapPins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Markers_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", nullable: true),
                    Margin = table.Column<int>(type: "INTEGER", nullable: true),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    Text = table.Column<string>(type: "varchar(1000)", nullable: true),
                    ImgSrc = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Autoplay = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    Playlist = table.Column<string>(type: "varchar(1000)", nullable: true),
                    MapSrc = table.Column<string>(type: "varchar(500)", nullable: true),
                    mapHeight = table.Column<int>(type: "INTEGER", nullable: true),
                    MapId = table.Column<int>(type: "INTEGER", nullable: true),
                    ViewId = table.Column<int>(type: "INTEGER", nullable: false),
                    YearId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_Coordinates_MapId",
                table: "Coordinates",
                column: "MapId");

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
                name: "IX_Markers_MapId",
                table: "Markers",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_PinId",
                table: "Markers",
                column: "PinId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Pilgrimages_Pilgri~",
                table: "Accounts",
                column: "PilgrimageId",
                principalTable: "Pilgrimages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Pilgrimages_Pilgri~",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropTable(
                name: "MapPins");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Pilgrimages");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PilgrimageId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PilgrimageId",
                table: "Accounts");
        }
    }
}
