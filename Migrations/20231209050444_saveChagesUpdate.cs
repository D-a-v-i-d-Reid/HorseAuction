using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorseAuction.Migrations
{
    /// <inheritdoc />
    public partial class saveChagesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidderName",
                table: "Bids");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BidderName",
                table: "Bids",
                type: "TEXT",
                nullable: true);
        }
    }
}
