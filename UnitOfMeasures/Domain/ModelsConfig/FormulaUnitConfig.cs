using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Domain.ModelsConfig
{
    public class FormulaUnitConfig : IEntityTypeConfiguration<FormulaUnit>
    {
        public void Configure(EntityTypeBuilder<FormulaUnit> builder)
        {
            builder.ToTable("FormulaUnits");

            builder.HasKey("Id").HasName("FormulaUnitID");

            builder.Property(bm => bm.Id).HasColumnName("FormulaUnitID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(bm => bm.Name).HasMaxLength(50).IsRequired();
            builder.Property(bm => bm.Code).HasMaxLength(10).IsRequired();
            builder.Property(bm => bm.BaseMeasuremenID).IsRequired();
            builder.Property(bm => bm.ConvertFromBaseFormula).IsRequired();
            builder.Property(bm => bm.ConvertToBaseFormula).IsRequired();
        }
    }
}
