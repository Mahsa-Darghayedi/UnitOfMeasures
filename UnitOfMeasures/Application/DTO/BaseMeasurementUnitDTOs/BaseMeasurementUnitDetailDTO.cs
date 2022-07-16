using UnitOfMeasures.Application.DTO.MeasurementDimensionDTOs;
using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.DTO.BaseMeasurementUnitDTOs
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
