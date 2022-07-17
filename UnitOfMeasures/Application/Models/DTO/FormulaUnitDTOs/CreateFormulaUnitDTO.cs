using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Utilities;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.Validations;

namespace UnitOfMeasures.Application.Models.DTO.FormulaUnitDTOs
{
    public class CreateFormulaUnitDTO
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required(AllowEmptyStrings = false)]
        [FormulaValidator]
        public string ConvertFromBaseFormula { get; set; }
        [Required(AllowEmptyStrings = false)]
        [FormulaValidator]
        public string ConvertToBaseFormula { get; set; }
        [Required]
        public BaseMeasurementUnitDetailDTO BaseMeasurementUnitDetailDTO { get; set; }

    }
}
