using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class CoefficientUnit 
    {
        public int Id { get; set; }
        public float Ratio { get; set; }

        public ChildUnit ChildUnit { get; set; }
    }
}
