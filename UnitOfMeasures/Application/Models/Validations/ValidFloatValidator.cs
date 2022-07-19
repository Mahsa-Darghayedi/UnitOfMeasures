using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Models.Validations
{
    public class ValidFloatValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            else if (value is float) return true;
            else return false;
        }
    }
}
