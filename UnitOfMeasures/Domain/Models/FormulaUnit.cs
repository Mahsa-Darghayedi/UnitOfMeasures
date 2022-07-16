using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class FormulaUnit : BaseEntity<int>
    {
        public int BaseMeasuremenID { get; set; }
        public string ConvertFromBaseFormula { get; set; }
        public string ConvertToBaseFormula { get; set; }
    }
}
