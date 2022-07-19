using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.Validations;

namespace UnitOfMeasures.Application.Models.DTO
{
    public class ConvertDTO
    {
        [Required]
        [ValidFloatValidator]
        public float Value { get; set; }
        [Required(AllowEmptyStrings = false)]
        public UnitsDTO From { get; set; }
        [Required(AllowEmptyStrings = false)]
        public UnitsDTO To { get; set; }
    }
}
