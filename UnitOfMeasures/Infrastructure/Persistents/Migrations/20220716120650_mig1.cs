using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnitOfMeasures.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormulaUnits",
                columns: table => new
                {
                    FormulaUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseMeasuremenID = table.Column<int>(type: "int", nullable: false),
                    ConvertFromBaseFormula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConvertToBaseFormula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FormulaUnitID", x => x.FormulaUnitID);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementDimensions",
                columns: table => new
                {
                    MeasurementDimensionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MeasurementDimensionID", x => x.MeasurementDimensionID);
                });

            migrationBuilder.CreateTable(
                name: "BaseMeasurementUnits",
                columns: table => new
                {
                    BaseMeasurementUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeasurementDimensionID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BaseMeasurementUnitID", x => x.BaseMeasurementUnitID);
                    table.ForeignKey(
                        name: "FK_BaseMeasurementUnits_MeasurementDimensions_MeasurementDimensionID",
                        column: x => x.MeasurementDimensionID,
                        principalTable: "MeasurementDimensions",
                        principalColumn: "MeasurementDimensionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoefficientUnits",
                columns: table => new
                {
                    CoefficientUnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseMeasuremenID = table.Column<int>(type: "int", nullable: false),
                    Ratio = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CoefficientUnitID", x => x.CoefficientUnitID);
                    table.ForeignKey(
                        name: "FK_CoefficientUnits_BaseMeasurementUnits_BaseMeasuremenID",
                        column: x => x.BaseMeasuremenID,
                        principalTable: "BaseMeasurementUnits",
                        principalColumn: "BaseMeasurementUnitID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMeasurementUnits_MeasurementDimensionID",
                table: "BaseMeasurementUnits",
                column: "MeasurementDimensionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoefficientUnits_BaseMeasuremenID",
                table: "CoefficientUnits",
                column: "BaseMeasuremenID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoefficientUnits");

            migrationBuilder.DropTable(
                name: "FormulaUnits");

            migrationBuilder.DropTable(
                name: "BaseMeasurementUnits");

            migrationBuilder.DropTable(
                name: "MeasurementDimensions");
        }
    }
}
