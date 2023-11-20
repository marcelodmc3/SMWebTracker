using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    public partial class simplify_tracker_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperMetroidGameTrackers");

            migrationBuilder.AddColumn<Guid>(
                name: "SuperMetroidGameId",
                table: "SuperMetroidTrackers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SuperMetroidTrackers_SuperMetroidGameId",
                table: "SuperMetroidTrackers",
                column: "SuperMetroidGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperMetroidTrackers_SuperMetroidGames_SuperMetroidGameId",
                table: "SuperMetroidTrackers",
                column: "SuperMetroidGameId",
                principalTable: "SuperMetroidGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperMetroidTrackers_SuperMetroidGames_SuperMetroidGameId",
                table: "SuperMetroidTrackers");

            migrationBuilder.DropIndex(
                name: "IX_SuperMetroidTrackers_SuperMetroidGameId",
                table: "SuperMetroidTrackers");

            migrationBuilder.DropColumn(
                name: "SuperMetroidGameId",
                table: "SuperMetroidTrackers");

            migrationBuilder.CreateTable(
                name: "SuperMetroidGameTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuperMetroidTrackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuperMetroidGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMetroidGameTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperMetroidGameTrackers_SuperMetroidGames_SuperMetroidGameId",
                        column: x => x.SuperMetroidGameId,
                        principalTable: "SuperMetroidGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuperMetroidGameTrackers_SuperMetroidTrackers_SuperMetroidTrackerId",
                        column: x => x.SuperMetroidTrackerId,
                        principalTable: "SuperMetroidTrackers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperMetroidGameTrackers_SuperMetroidGameId",
                table: "SuperMetroidGameTrackers",
                column: "SuperMetroidGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperMetroidGameTrackers_SuperMetroidTrackerId",
                table: "SuperMetroidGameTrackers",
                column: "SuperMetroidTrackerId");
        }
    }
}
