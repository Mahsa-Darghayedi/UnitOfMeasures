using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class ChildUnit : BaseEntity<int>
    {
        public int BaseMeasuremenID { get; set; }
        public virtual BaseMeasurementUnit BaseMeasurementUnit { get; set; }
               
    }
}
