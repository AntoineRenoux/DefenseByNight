using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class Attribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributeAttributKey",
                table: "Focus",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributKey = table.Column<string>(nullable: false),
                    AttributName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.AttributKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Focus_AttributeAttributKey",
                table: "Focus",
                column: "AttributeAttributKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Focus_Attributes_AttributeAttributKey",
                table: "Focus",
                column: "AttributeAttributKey",
                principalTable: "Attributes",
                principalColumn: "AttributKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Focus_Attributes_AttributeAttributKey",
                table: "Focus");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Focus_AttributeAttributKey",
                table: "Focus");

            migrationBuilder.DropColumn(
                name: "AttributeAttributKey",
                table: "Focus");
        }
    }
}
