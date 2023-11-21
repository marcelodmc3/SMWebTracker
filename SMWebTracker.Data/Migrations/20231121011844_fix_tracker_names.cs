using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    public partial class fix_tracker_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ridey",
                table: "SuperMetroidTrackers",
                newName: "Ridley");

            migrationBuilder.RenameColumn(
                name: "BombJump",
                table: "SuperMetroidTrackers",
                newName: "Bombs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ridley",
                table: "SuperMetroidTrackers",
                newName: "Ridey");

            migrationBuilder.RenameColumn(
                name: "Bombs",
                table: "SuperMetroidTrackers",
                newName: "BombJump");
        }
    }
}
