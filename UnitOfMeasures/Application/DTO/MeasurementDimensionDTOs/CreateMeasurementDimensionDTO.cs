using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.DTO.MeasurementDimensionDTOs

{
    public class CreateMeasurementDimensionDTO
    {
        [Required(AllowEmptyStrings =false)]
        public string Name { get; set; }
    }
}
