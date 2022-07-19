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

            builder.Property(bm => bm.Id).HasColumnName("FormulaUnitID").IsRequired();
            builder.Property(bm => bm.ConvertFromBaseFormula).IsRequired();
            builder.Property(bm => bm.ConvertToBaseFormula).IsRequired();

            builder.HasOne(c => c.ChildUnit).WithOne().HasForeignKey<FormulaUnit>(b=> b.Id);
            builder.HasIndex(c => c.Id).IsUnique();

        }
    }
}
