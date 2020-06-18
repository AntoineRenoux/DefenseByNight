using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class Character : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterKey = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SectAffiliateKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterKey);
                    table.ForeignKey(
                        name: "FK_Characters_Affiliates_SectAffiliateKey",
                        column: x => x.SectAffiliateKey,
                        principalTable: "Affiliates",
                        principalColumn: "AffiliateKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SectAffiliateKey",
                table: "Characters",
                column: "SectAffiliateKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
