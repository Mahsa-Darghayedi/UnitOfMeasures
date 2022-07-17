using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Models.DTO.MeasurementDimensionDTOs

{
    public class CreateMeasurementDimensionDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
