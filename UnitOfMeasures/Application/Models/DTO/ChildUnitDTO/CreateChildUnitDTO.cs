using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.DTO.BaseMeasurementUnitDTOs;

namespace UnitOfMeasures.Application.Models.DTO.ChildUnitDTO
{
    public class CreateChildUnitDTO
    {

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        public BaseMeasurementUnitDetailDTO BaseMeasurementUnitDetailDTO { get; set; }

    }
}
