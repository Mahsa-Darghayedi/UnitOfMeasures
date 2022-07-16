using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class BaseMeasurementUnit : BaseEntity<int>
    {
        public int MeasurementDimensionID { get; set;}
    }
}
