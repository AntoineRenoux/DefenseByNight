using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class Chronicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chronicles",
                columns: table => new
                {
                    ChronicleKey = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chronicles", x => x.ChronicleKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chronicles");
        }
    }
}
