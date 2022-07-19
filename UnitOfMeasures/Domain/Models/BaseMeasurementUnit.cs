using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class BaseMeasurementUnit : BaseEntity<int>
    {
        public string MeasurementDimensionName { get; set; }

        public IReadOnlyCollection<ChildUnit> ChildUnits { get; set;}
    }
}
