using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RickAndMortyApiCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Location_LocationId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Location_LocationId1",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_LocationId1",
                table: "Characters",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_LocationId1",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Location_LocationId",
                table: "Characters",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Location_LocationId1",
                table: "Characters",
                column: "LocationId1",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
