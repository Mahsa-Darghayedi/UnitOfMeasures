using System.ComponentModel.DataAnnotations;

namespace UnitOfMeasures.Application.Validations
{
    public class FormulaValidator : ValidationAttribute
    {
        private bool isFormulaValid(string formula)
        {
            if (string.IsNullOrEmpty(formula))
                return false;

            if (string.IsNullOrWhiteSpace(formula))
                return false;

            var invalidVariableLetter = formula.ToCharArray().Any(c => char.IsLetter(c) && c != 'a');
            if (invalidVariableLetter)
                return false;

            return true;

        }


        public override bool IsValid(object? value)
        {
           string formula = value == null ? string.Empty : value.ToString();
            return isFormulaValid(formula);    
        }
    }
}
