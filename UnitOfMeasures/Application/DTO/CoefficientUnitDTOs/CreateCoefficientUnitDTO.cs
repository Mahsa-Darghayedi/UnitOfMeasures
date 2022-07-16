using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.DTO.BaseMeasurementUnitDTOs;

namespace UnitOfMeasures.Application.DTO.CoefficientUnitDTOs
{
    public class CreateCoefficientUnitDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }
        [Required]
        public float Ratio { get; set; }
        [Required]
        public BaseMeasurementUnitDetailDTO BaseMeasurementUnitDetailDTO { get; set; }

    }
}
