using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fashion_bingo_api.Migrations
{
    /// <inheritdoc />
    public partial class AddPowerUpFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoDaubBoosts",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeDaubTokens",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoDaubBoosts",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "FreeDaubTokens",
                table: "Player");
        }
    }
}
