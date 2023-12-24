using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorseAuction.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerToHorse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Horses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Horses");
        }
    }
}
