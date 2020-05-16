using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class TraductionCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Traductions",
                table: "Traductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traductions",
                table: "Traductions",
                columns: new[] { "Key", "LCID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Traductions",
                table: "Traductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traductions",
                table: "Traductions",
                column: "Key");
        }
    }
}
