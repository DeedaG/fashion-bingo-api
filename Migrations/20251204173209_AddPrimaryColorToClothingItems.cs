using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fashion_bingo_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryColorToClothingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Economy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Gems = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Economy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mannequin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mannequin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    CurrentMannequinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EconomyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Economy_EconomyId",
                        column: x => x.EconomyId,
                        principalTable: "Economy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Player_Mannequin_CurrentMannequinId",
                        column: x => x.CurrentMannequinId,
                        principalTable: "Mannequin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingItem_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MysteryBoxReward",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Gems = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MysteryBoxReward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MysteryBoxReward_ClothingItem_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "ClothingItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MysteryBoxReward_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItem_PlayerId",
                table: "ClothingItem",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MysteryBoxReward_ClothingId",
                table: "MysteryBoxReward",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_MysteryBoxReward_PlayerId",
                table: "MysteryBoxReward",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_CurrentMannequinId",
                table: "Player",
                column: "CurrentMannequinId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_EconomyId",
                table: "Player",
                column: "EconomyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MysteryBoxReward");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ClothingItem");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Economy");

            migrationBuilder.DropTable(
                name: "Mannequin");
        }
    }
}
