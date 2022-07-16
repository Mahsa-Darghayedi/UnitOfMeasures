using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class MeasurementDimension 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IReadOnlyCollection<BaseMeasurementUnit> BaseMeasurementUnits { get; set; }
    }
}
