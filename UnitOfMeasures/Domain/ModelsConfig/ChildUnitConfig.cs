using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Domain.ModelsConfig
{
    public class ChildUnitConfig : IEntityTypeConfiguration<ChildUnit>
    {
        public void Configure(EntityTypeBuilder<ChildUnit> builder)
        {
            builder.ToTable("ChildUnits");
            builder.HasKey("Id").HasName("ChildUnitID");
            builder.HasIndex("Code").IsUnique();

            builder.Property(bm => bm.Id).HasColumnName("ChildUnitID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(bm => bm.Code).HasMaxLength(10).IsRequired();
            builder.Property(bm => bm.Name).HasMaxLength(50).IsRequired();
            builder.Property(bm => bm.BaseMeasuremenID).IsRequired();
        }
    }
}
