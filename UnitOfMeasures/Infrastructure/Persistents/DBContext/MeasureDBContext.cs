using Microsoft.EntityFrameworkCore;
using UnitOfMeasures.Domain.Models;

namespace UnitOfMeasures.Infrastructure.Persistents.DBContext
{
    public class MeasureDBContext : DbContext
    {

        public MeasureDBContext(DbContextOptions options) : base(options) { }

        public DbSet<MeasurementDimension> MeasurementDimensions { get; set; }
        public DbSet<BaseMeasurementUnit> BaseMeasurementUnits  { get; set; }
        public DbSet<CoefficientUnit> CoefficientUnits  { get; set; }
        public DbSet<FormulaUnit> FormulaUnits  { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MeasureDBContext).Assembly);
            base.OnModelCreating(builder);  
        }
    }
}
