using UnitOfMeasures.Application.DTO.BaseMeasurementUnitDTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.DTO.FormulaUnitDTOs
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
        public string ConvertFromBaseFormula { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ConvertToBaseFormula { get; set; }
        [Required]
        public BaseMeasurementUnitDetailDTO BaseMeasurementUnitDetailDTO { get; set; }

    }
}
