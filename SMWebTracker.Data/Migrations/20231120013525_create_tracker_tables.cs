using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMWebTracker.Data.Migrations
{
    public partial class create_tracker_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperMetroidGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMetroidGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperMetroidTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerIndex = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VariaSuit = table.Column<bool>(type: "bit", nullable: false),
                    GravitySuit = table.Column<bool>(type: "bit", nullable: false),
                    ChargeBeam = table.Column<bool>(type: "bit", nullable: false),
                    IceBeam = table.Column<bool>(type: "bit", nullable: false),
                    WaveBeam = table.Column<bool>(type: "bit", nullable: false),
                    SpazerBeam = table.Column<bool>(type: "bit", nullable: false),
                    PlasmaBeam = table.Column<bool>(type: "bit", nullable: false),
                    MorphBall = table.Column<bool>(type: "bit", nullable: false),
                    BombJump = table.Column<bool>(type: "bit", nullable: false),
                    HighJumpBoots = table.Column<bool>(type: "bit", nullable: false),
                    SpeedBooster = table.Column<bool>(type: "bit", nullable: false),
                    SpaceJump = table.Column<bool>(type: "bit", nullable: false),
                    SpringBall = table.Column<bool>(type: "bit", nullable: false),
                    Kraid = table.Column<bool>(type: "bit", nullable: false),
                    Phantoon = table.Column<bool>(type: "bit", nullable: false),
                    Draygon = table.Column<bool>(type: "bit", nullable: false),
                    Ridey = table.Column<bool>(type: "bit", nullable: false),
                    ScrewAttack = table.Column<bool>(type: "bit", nullable: false),
                    SporeSpawn = table.Column<bool>(type: "bit", nullable: false),
                    Crocomire = table.Column<bool>(type: "bit", nullable: false),
                    Botwoon = table.Column<bool>(type: "bit", nullable: false),
                    GoldenTorizo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperMetroidTrackers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperMetroidGameTrackers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuperMetroidGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuperMetroidTrackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperMetroidGameTrackers");

            migrationBuilder.DropTable(
                name: "SuperMetroidGames");

            migrationBuilder.DropTable(
                name: "SuperMetroidTrackers");
        }
    }
}
