using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RickAndMortyApiCrawler.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLocationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Locations");
        }
    }
}
