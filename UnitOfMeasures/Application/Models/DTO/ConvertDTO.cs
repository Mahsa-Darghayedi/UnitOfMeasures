using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.Validations;

namespace UnitOfMeasures.Application.Models.DTO
{
    public class ConvertDTO
    {
        [Required]

        [ValidDoubleValidator]
        public double Value { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FromCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ToCode { get; set; }

  
    }
}
