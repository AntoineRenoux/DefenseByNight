using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class AffilationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Affiliates",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Affiliates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Affiliates");
        }
    }
}
