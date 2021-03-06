// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitOfMeasures.Infrastructure.Persistents.DBContext;

#nullable disable

namespace UnitOfMeasures.Migrations
{
    [DbContext(typeof(MeasureDBContext))]
    [Migration("20220719112107_m2")]
    partial class m2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.BaseMeasurementUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BaseMeasurementUnitID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MeasurementDimensionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("BaseMeasurementUnitID");

                    b.HasIndex("MeasurementDimensionName")
                        .IsUnique();

                    b.ToTable("BaseMeasurementUnits", (string)null);
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.ChildUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChildUnitID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BaseMeasuremenID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("ChildUnitID");

                    b.HasIndex("BaseMeasuremenID");

                    b.ToTable("ChildUnits", (string)null);
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.CoefficientUnit", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("CoefficientUnitID");

                    b.Property<int>("ChildUnitId")
                        .HasColumnType("int");

                    b.Property<float>("Ratio")
                        .HasColumnType("real");

                    b.HasKey("Id")
                        .HasName("CoefficientUnitID");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("CoefficientUnits", (string)null);
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.FormulaUnit", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("FormulaUnitID");

                    b.Property<int>("ChildUnitId")
                        .HasColumnType("int");

                    b.Property<string>("ConvertFromBaseFormula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConvertToBaseFormula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("FormulaUnitID");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("FormulaUnits", (string)null);
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.ChildUnit", b =>
                {
                    b.HasOne("UnitOfMeasures.Domain.Models.BaseMeasurementUnit", "BaseMeasurementUnit")
                        .WithMany("ChildUnits")
                        .HasForeignKey("BaseMeasuremenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseMeasurementUnit");
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.CoefficientUnit", b =>
                {
                    b.HasOne("UnitOfMeasures.Domain.Models.ChildUnit", "ChildUnit")
                        .WithOne()
                        .HasForeignKey("UnitOfMeasures.Domain.Models.CoefficientUnit", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildUnit");
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.FormulaUnit", b =>
                {
                    b.HasOne("UnitOfMeasures.Domain.Models.ChildUnit", "ChildUnit")
                        .WithOne()
                        .HasForeignKey("UnitOfMeasures.Domain.Models.FormulaUnit", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildUnit");
                });

            modelBuilder.Entity("UnitOfMeasures.Domain.Models.BaseMeasurementUnit", b =>
                {
                    b.Navigation("ChildUnits");
                });
#pragma warning restore 612, 618
        }
    }
}
