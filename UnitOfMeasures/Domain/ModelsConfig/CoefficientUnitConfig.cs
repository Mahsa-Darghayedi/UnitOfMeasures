using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Domain.ModelsConfig
{
    public class CoefficientUnitConfig : IEntityTypeConfiguration<CoefficientUnit>
    {
        public void Configure(EntityTypeBuilder<CoefficientUnit> builder)
        {
            builder.ToTable("CoefficientUnits");

            builder.HasKey("Id").HasName("CoefficientUnitID");

            builder.Property(bm => bm.Id).HasColumnName("CoefficientUnitID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(bm => bm.Name).HasMaxLength(50).IsRequired();
            builder.Property(bm => bm.Code).HasMaxLength(10).IsRequired();
            builder.Property(bm => bm.BaseMeasuremenID).IsRequired();

        }
    }
}
