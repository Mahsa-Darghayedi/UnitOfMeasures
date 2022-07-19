using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Models.Validations
{
    public class ValidDoubleValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            else if (value is double) return true;
            else return false;
        }
    }
}
