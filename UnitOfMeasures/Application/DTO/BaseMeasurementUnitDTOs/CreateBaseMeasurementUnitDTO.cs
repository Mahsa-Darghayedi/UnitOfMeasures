using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.DTO.MeasurementDimensionDTOs;

namespace UnitOfMeasures.Application.DTO.BaseMeasurementUnitDTOs
{
    public class CreateBaseMeasurementUnitDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }
        [Required]
        public MeasurementDimensionDetailDTO MeasurementDimension { get; set; }
    }
}
