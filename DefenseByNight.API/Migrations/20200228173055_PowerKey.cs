using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class PowerKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Powers",
                table: "Powers");

            migrationBuilder.AlterColumn<string>(
                name: "PowerName",
                table: "Powers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PowerKey",
                table: "Powers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Powers",
                table: "Powers",
                column: "PowerKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Powers",
                table: "Powers");

            migrationBuilder.DropColumn(
                name: "PowerKey",
                table: "Powers");

            migrationBuilder.AlterColumn<string>(
                name: "PowerName",
                table: "Powers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Powers",
                table: "Powers",
                column: "PowerName");
        }
    }
}
