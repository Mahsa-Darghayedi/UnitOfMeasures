using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Domain.ModelsConfig
{
    public class MeasurementDimensionConfig : IEntityTypeConfiguration<MeasurementDimension>
    {
        public void Configure(EntityTypeBuilder<MeasurementDimension> builder)
        {
             builder.ToTable("MeasurementDimensions");

            builder.HasKey("Id").HasName("MeasurementDimensionID");
            builder.Property(measure => measure.Id).HasColumnName("MeasurementDimensionID").IsRequired().ValueGeneratedOnAdd();


            builder.HasMany(measure => measure.BaseMeasurementUnits)
                .WithOne(bm => bm.MeasurementDimension)
                .HasForeignKey(measure => measure.MeasurementDimensionID);
        }
    }
}
