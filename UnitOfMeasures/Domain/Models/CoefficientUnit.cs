using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class CoefficientUnit : BaseEntity<int>
    {
        public int BaseMeasuremenID { get; set; }
        public float Ratio { get; set; }

        public virtual BaseMeasurementUnit BaseMeasurementUnit { get; set; }
    }
}
