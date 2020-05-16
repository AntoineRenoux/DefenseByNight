using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class ClanAndAtouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClanKey",
                table: "Disciplines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clans",
                columns: table => new
                {
                    ClanKey = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    History = table.Column<string>(nullable: false),
                    Organisation = table.Column<string>(nullable: false),
                    Weakness = table.Column<string>(nullable: false),
                    RarityClan = table.Column<int>(nullable: false),
                    Affiliate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clans", x => x.ClanKey);
                });

            migrationBuilder.CreateTable(
                name: "Atouts",
                columns: table => new
                {
                    AtoutKey = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ClanKey = table.Column<string>(nullable: true),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atouts", x => x.AtoutKey);
                    table.ForeignKey(
                        name: "FK_Atouts_Clans_ClanKey",
                        column: x => x.ClanKey,
                        principalTable: "Clans",
                        principalColumn: "ClanKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_ClanKey",
                table: "Disciplines",
                column: "ClanKey");

            migrationBuilder.CreateIndex(
                name: "IX_Atouts_ClanKey",
                table: "Atouts",
                column: "ClanKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Clans_ClanKey",
                table: "Disciplines",
                column: "ClanKey",
                principalTable: "Clans",
                principalColumn: "ClanKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Clans_ClanKey",
                table: "Disciplines");

            migrationBuilder.DropTable(
                name: "Atouts");

            migrationBuilder.DropTable(
                name: "Clans");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_ClanKey",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "ClanKey",
                table: "Disciplines");
        }
    }
}
