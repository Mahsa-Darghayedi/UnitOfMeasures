using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Utilities;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.Validations;
using UnitOfMeasures.Application.Models.DTO.ChildUnitDTO;

namespace UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs
{
    public class CreateFormulaUnitDTO : CreateChildUnitDTO
    {
        [Required(AllowEmptyStrings = false)]
        [FormulaValidator]
        public string ConvertFromBaseFormula { get; set; }
        [Required(AllowEmptyStrings = false)]
        [FormulaValidator]
        public string ConvertToBaseFormula { get; set; }
    }
}
