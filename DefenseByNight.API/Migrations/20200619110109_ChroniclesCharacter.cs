using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class ChroniclesCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Chronicles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChronicleKey",
                table: "Characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ChronicleKey",
                table: "Characters",
                column: "ChronicleKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Chronicles_ChronicleKey",
                table: "Characters",
                column: "ChronicleKey",
                principalTable: "Chronicles",
                principalColumn: "ChronicleKey",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Chronicles_ChronicleKey",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ChronicleKey",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Chronicles");

            migrationBuilder.DropColumn(
                name: "ChronicleKey",
                table: "Characters");
        }
    }
}
