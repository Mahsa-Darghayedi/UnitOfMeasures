namespace UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs
{
    public class FormulaUnitDetailDTO : CreateFormulaUnitDTO
    {

        public string ConvertFromBaseFormula { get; set; }

        public string ConvertToBaseFormula { get; set; }
    }
}
