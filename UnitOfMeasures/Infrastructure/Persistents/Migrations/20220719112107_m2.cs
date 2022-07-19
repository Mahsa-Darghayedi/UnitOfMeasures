using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitOfMeasures.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseMeasurementUnits",
                columns: table => new
                {
                    BaseMeasurementUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementDimensionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseMeasurementUnitID", x => x.BaseMeasurementUnitID);
                });

            migrationBuilder.CreateTable(
                name: "ChildUnits",
                columns: table => new
                {
                    ChildUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseMeasuremenID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ChildUnitID", x => x.ChildUnitID);
                    table.ForeignKey(
                        name: "FK_ChildUnits_BaseMeasurementUnits_BaseMeasuremenID",
                        column: x => x.BaseMeasuremenID,
                        principalTable: "BaseMeasurementUnits",
                        principalColumn: "BaseMeasurementUnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoefficientUnits",
                columns: table => new
                {
                    CoefficientUnitID = table.Column<int>(type: "int", nullable: false),
                    ChildUnitId = table.Column<int>(type: "int", nullable: false),
                    Ratio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CoefficientUnitID", x => x.CoefficientUnitID);
                    table.ForeignKey(
                        name: "FK_CoefficientUnits_ChildUnits_CoefficientUnitID",
                        column: x => x.CoefficientUnitID,
                        principalTable: "ChildUnits",
                        principalColumn: "ChildUnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormulaUnits",
                columns: table => new
                {
                    FormulaUnitID = table.Column<int>(type: "int", nullable: false),
                    ChildUnitId = table.Column<int>(type: "int", nullable: false),
                    ConvertFromBaseFormula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConvertToBaseFormula = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FormulaUnitID", x => x.FormulaUnitID);
                    table.ForeignKey(
                        name: "FK_FormulaUnits_ChildUnits_FormulaUnitID",
                        column: x => x.FormulaUnitID,
                        principalTable: "ChildUnits",
                        principalColumn: "ChildUnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMeasurementUnits_MeasurementDimensionName",
                table: "BaseMeasurementUnits",
                column: "MeasurementDimensionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChildUnits_BaseMeasuremenID",
                table: "ChildUnits",
                column: "BaseMeasuremenID");

            migrationBuilder.CreateIndex(
                name: "IX_CoefficientUnits_CoefficientUnitID",
                table: "CoefficientUnits",
                column: "CoefficientUnitID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormulaUnits_FormulaUnitID",
                table: "FormulaUnits",
                column: "FormulaUnitID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoefficientUnits");

            migrationBuilder.DropTable(
                name: "FormulaUnits");

            migrationBuilder.DropTable(
                name: "ChildUnits");

            migrationBuilder.DropTable(
                name: "BaseMeasurementUnits");
        }
    }
}
