using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class Health : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Health",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allergies = table.Column<string>(nullable: true),
                    ContectFirstName = table.Column<string>(nullable: false),
                    ContactLastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Health", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HealthId",
                table: "AspNetUsers",
                column: "HealthId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Health_HealthId",
                table: "AspNetUsers",
                column: "HealthId",
                principalTable: "Health",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Health_HealthId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Health");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HealthId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HealthId",
                table: "AspNetUsers");
        }
    }
}
