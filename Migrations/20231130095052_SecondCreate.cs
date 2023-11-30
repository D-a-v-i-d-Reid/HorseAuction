using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HorseAuction.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Horses",
                columns: new[] { "HorseId", "Age", "Color", "Description", "HorseName", "PerformanceType", "StartingBid" },
                values: new object[,]
                {
                    { 1, 3, "Red Dun", "Easy Keeper. Fun to be Around", "Genetic", "Western Pleasure", 3500m },
                    { 2, 4, "Bay with a snip", "Barn sour and will kick you", "Rowdy", "Reining", 5000m },
                    { 3, 2, "Brown", "Very green, just started under saddle", "Brownie", "Western Pleasure", 10000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "HorseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "HorseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "HorseId",
                keyValue: 3);
        }
    }
}
