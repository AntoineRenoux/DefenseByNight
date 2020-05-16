using Microsoft.EntityFrameworkCore.Migrations;

namespace DefenseByNight.API.Migrations
{
    public partial class Disciplines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Focus_Attributes_AttributeAttributKey",
                table: "Focus");

            migrationBuilder.DropIndex(
                name: "IX_Focus_AttributeAttributKey",
                table: "Focus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeAttributKey",
                table: "Focus");

            migrationBuilder.DropColumn(
                name: "AttributKey",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributName",
                table: "Attributes");

            migrationBuilder.AddColumn<string>(
                name: "AttributeKey",
                table: "Focus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeKey",
                table: "Attributes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "Attributes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                column: "AttributeKey");

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    DisciplineKey = table.Column<string>(nullable: false),
                    DisciplineName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    TestScore = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.DisciplineKey);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    PowerName = table.Column<string>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    System = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FocusKey = table.Column<string>(nullable: true),
                    FocusEffect = table.Column<string>(nullable: true),
                    ExceptionalSuccess = table.Column<string>(nullable: true),
                    DisciplineKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.PowerName);
                    table.ForeignKey(
                        name: "FK_Powers_Disciplines_DisciplineKey",
                        column: x => x.DisciplineKey,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Powers_Focus_FocusKey",
                        column: x => x.FocusKey,
                        principalTable: "Focus",
                        principalColumn: "FocusKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Focus_AttributeKey",
                table: "Focus",
                column: "AttributeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_DisciplineKey",
                table: "Powers",
                column: "DisciplineKey");

            migrationBuilder.CreateIndex(
                name: "IX_Powers_FocusKey",
                table: "Powers",
                column: "FocusKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Focus_Attributes_AttributeKey",
                table: "Focus",
                column: "AttributeKey",
                principalTable: "Attributes",
                principalColumn: "AttributeKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Focus_Attributes_AttributeKey",
                table: "Focus");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Focus_AttributeKey",
                table: "Focus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeKey",
                table: "Focus");

            migrationBuilder.DropColumn(
                name: "AttributeKey",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "Attributes");

            migrationBuilder.AddColumn<string>(
                name: "AttributeAttributKey",
                table: "Focus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributKey",
                table: "Attributes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributName",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                column: "AttributKey");

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
    }
}
