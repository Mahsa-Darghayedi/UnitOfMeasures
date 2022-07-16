using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.DTO.MeasurementDimensionDTOs
{
    public class MeasurementDimensionDetailDTO
    {
        [Required]
        public int ID { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Name { get; set; }
    }
}
