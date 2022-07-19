using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitOfMeasures.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildUnitId",
                table: "FormulaUnits");

            migrationBuilder.DropColumn(
                name: "ChildUnitId",
                table: "CoefficientUnits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildUnitId",
                table: "FormulaUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildUnitId",
                table: "CoefficientUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
