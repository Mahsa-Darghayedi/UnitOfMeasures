using UnitOfMeasures.Domain.Models.BaseEntities;

namespace UnitOfMeasures.Domain.Models
{
    public class FormulaUnit 
    {
        public int Id { get; set; }
        public string ConvertFromBaseFormula { get; set; }
        public string ConvertToBaseFormula { get; set; }

        public ChildUnit ChildUnit { get; set; }    
    }
}
