using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StonksCore.Migrations
{
    public partial class OnMarketFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OnMarketFrom",
                table: "Tickers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnMarketFrom",
                table: "Tickers");
        }
    }
}
