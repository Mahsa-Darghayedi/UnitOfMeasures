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

            builder.Property(bm => bm.Id).HasColumnName("CoefficientUnitID").IsRequired();
            builder.Property(bm => bm.Ratio).IsRequired();

            builder.HasOne(c => c.ChildUnit).WithOne().HasForeignKey<CoefficientUnit>(b => b.Id);
            builder.HasIndex(c => c.Id).IsUnique();

        }
    }
}
