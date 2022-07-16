using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Domain.ModelsConfig
{
    public class BaseMeasurementUnitConfig : IEntityTypeConfiguration<BaseMeasurementUnit>
    {
        public void Configure(EntityTypeBuilder<BaseMeasurementUnit> builder)
        {
            builder.ToTable("BaseMeasurementUnits");

            builder.HasKey("Id").HasName("BaseMeasurementUnitID");
            builder.HasIndex(bm => bm.MeasurementDimensionID).IsUnique();

            builder.Property(bm => bm.Id).HasColumnName("BaseMeasurementUnitID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(bm => bm.Name).HasMaxLength(50).IsRequired();
            builder.Property(bm => bm.Code).HasMaxLength(10).IsRequired();
            builder.Property(bm => bm.MeasurementDimensionID).IsRequired();


            builder.HasMany(bm => bm.CoefficientUnits)
                .WithOne(cu => cu.BaseMeasurementUnit)
                .HasForeignKey(cu => cu.BaseMeasuremenID);

        }


    }
}
