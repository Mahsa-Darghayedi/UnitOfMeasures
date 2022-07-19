using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;
using UnitOfMeasures.Application.Models.DTO.ChildUnitDTO;

namespace UnitOfMeasures.Application.Models.DTO.CoefficientUnitDTOs
{
    public class CreateCoefficientUnitDTO : CreateChildUnitDTO
    {
        [Required]
        public float Ratio { get; set; }

    }
}
