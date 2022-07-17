using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.DTO.MeasurementDimensionDTOs;

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
        public MeasurementDimensionDetailDTO MeasurementDimension { get; set; }
    }
}
