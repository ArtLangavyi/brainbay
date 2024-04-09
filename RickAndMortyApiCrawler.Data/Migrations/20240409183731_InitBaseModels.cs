using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RickAndMortyApiCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitBaseModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(2550)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterOrigins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dimension = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LinksToResidents = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2550)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CharacterOriginId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(2550)", maxLength: 2550, nullable: true),
                    LinksToEpisode = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CharacterOriginId1 = table.Column<int>(type: "int", nullable: true),
                    LocationId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterOrigins_CharacterOriginId",
                        column: x => x.CharacterOriginId,
                        principalTable: "CharacterOrigins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterOrigins_CharacterOriginId1",
                        column: x => x.CharacterOriginId1,
                        principalTable: "CharacterOrigins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Characters_Location_LocationId1",
                        column: x => x.LocationId1,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterOriginId",
                table: "Characters",
                column: "CharacterOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterOriginId1",
                table: "Characters",
                column: "CharacterOriginId1");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_LocationId",
                table: "Characters",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_LocationId1",
                table: "Characters",
                column: "LocationId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "CharacterOrigins");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
