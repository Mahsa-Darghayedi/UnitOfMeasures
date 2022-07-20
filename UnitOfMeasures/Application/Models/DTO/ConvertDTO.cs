using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.Validations;

namespace UnitOfMeasures.Application.Models.DTO
{
    public class ConvertDTO
    {
        [Required]
<<<<<<< HEAD
        [ValidDoubleValidator]
        public double Value { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FromCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ToCode { get; set; }
=======
        [ValidFloatValidator]
        public float Value { get; set; }
        [Required(AllowEmptyStrings = false)]
        public UnitsDTO From { get; set; }
        [Required(AllowEmptyStrings = false)]
        public UnitsDTO To { get; set; }
>>>>>>> 925b1b0ff0c6466f1652976c58b895b8f73bf17c
    }
}
