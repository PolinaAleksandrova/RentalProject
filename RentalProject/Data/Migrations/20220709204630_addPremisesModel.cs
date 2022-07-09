using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalProject.Data.Migrations
{
    public partial class addPremisesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PremisesDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEquipAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PremisesTypeId = table.Column<int>(type: "int", nullable: false),
                    SpecialTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premises_PremisesTypes_PremisesTypeId",
                        column: x => x.PremisesTypeId,
                        principalTable: "PremisesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Premises_SpecialTags_SpecialTagId",
                        column: x => x.SpecialTagId,
                        principalTable: "SpecialTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Premises_PremisesTypeId",
                table: "Premises",
                column: "PremisesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_SpecialTagId",
                table: "Premises",
                column: "SpecialTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Premises");
        }
    }
}
