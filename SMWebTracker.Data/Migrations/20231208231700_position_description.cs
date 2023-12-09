using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    public partial class position_description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "SuperMetroidTrackers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SuperMetroidGames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "SuperMetroidTrackers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SuperMetroidGames");
        }
    }
}
