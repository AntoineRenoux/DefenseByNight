using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class FixHealth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContectFirstName",
                table: "Health");

            migrationBuilder.AddColumn<string>(
                name: "ContactFirstName",
                table: "Health",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactFirstName",
                table: "Health");

            migrationBuilder.AddColumn<string>(
                name: "ContectFirstName",
                table: "Health",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
