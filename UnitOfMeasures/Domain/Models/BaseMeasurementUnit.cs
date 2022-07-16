using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class BaseMeasurementUnit : BaseEntity<int>
    {
        public int MeasurementDimensionID { get; set;}

        public  MeasurementDimension MeasurementDimension { get; set;}


        public IReadOnlyCollection<CoefficientUnit> CoefficientUnits { get; set;}
    }
}
