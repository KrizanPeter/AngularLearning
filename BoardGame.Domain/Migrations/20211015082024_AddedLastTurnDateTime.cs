using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoardGame.Domain.Migrations
{
    public partial class AddedLastTurnDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastTurnChange",
                table: "Sessions",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTurnChange",
                table: "Sessions");
        }
    }
}
