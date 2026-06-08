using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotaParser.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsToFullOpenDota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDotaPlus",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LeaderboardRank",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RankTier",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Players_SteamAccountId",
                table: "Players",
                column: "SteamAccountId");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DotaMatchId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerSlot = table.Column<int>(type: "integer", nullable: false),
                    RadiantWin = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    GameMode = table.Column<int>(type: "integer", nullable: false),
                    LobbyType = table.Column<int>(type: "integer", nullable: false),
                    HeroId = table.Column<int>(type: "integer", nullable: false),
                    Kills = table.Column<int>(type: "integer", nullable: false),
                    Deaths = table.Column<int>(type: "integer", nullable: false),
                    Assists = table.Column<int>(type: "integer", nullable: false),
                    AverageRank = table.Column<int>(type: "integer", nullable: true),
                    LeaverStatus = table.Column<int>(type: "integer", nullable: false),
                    PartySize = table.Column<int>(type: "integer", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Players_PlayerAccountId",
                        column: x => x.PlayerAccountId,
                        principalTable: "Players",
                        principalColumn: "SteamAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_DotaMatchId",
                table: "Matches",
                column: "DotaMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerAccountId",
                table: "Matches",
                column: "PlayerAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Players_SteamAccountId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsDotaPlus",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LeaderboardRank",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RankTier",
                table: "Players");
        }
    }
}
