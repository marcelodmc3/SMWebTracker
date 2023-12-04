using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    public partial class xray_grapple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Grapple",
                table: "SuperMetroidTrackers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Xray",
                table: "SuperMetroidTrackers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grapple",
                table: "SuperMetroidTrackers");

            migrationBuilder.DropColumn(
                name: "Xray",
                table: "SuperMetroidTrackers");
        }
    }
}
