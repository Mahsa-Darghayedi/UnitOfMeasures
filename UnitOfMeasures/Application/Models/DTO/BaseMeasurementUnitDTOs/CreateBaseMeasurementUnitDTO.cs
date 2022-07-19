using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs
{
    public class CreateBaseMeasurementUnitDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }

        [Required]
        public string MeasurementDimensionName { get; set; }
    }
}
