using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs
{
    public class BaseMeasurementUnitDetailDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string MeasurementDimensionName { get; set; }
    }
}
