using Microsoft.EntityFrameworkCore.Migrations;

namespace StonksCore.Migrations
{
    public partial class Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickers_Isin",
                table: "Tickers",
                column: "Isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickers_Type",
                table: "Tickers",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickers_Isin",
                table: "Tickers");

            migrationBuilder.DropIndex(
                name: "IX_Tickers_Type",
                table: "Tickers");
        }
    }
}
