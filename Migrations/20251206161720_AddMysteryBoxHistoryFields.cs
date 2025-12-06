using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fashion_bingo_api.Migrations
{
    /// <inheritdoc />
    public partial class AddMysteryBoxHistoryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinsSpent",
                table: "MysteryBoxReward",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOpened",
                table: "MysteryBoxReward",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GemsSpent",
                table: "MysteryBoxReward",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "MysteryBoxReward",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinsSpent",
                table: "MysteryBoxReward");

            migrationBuilder.DropColumn(
                name: "DateOpened",
                table: "MysteryBoxReward");

            migrationBuilder.DropColumn(
                name: "GemsSpent",
                table: "MysteryBoxReward");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "MysteryBoxReward");
        }
    }
}
