using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using UnitOfMeasures.Application.Utilities;

namespace UnitOfMeasures.Application.Models.Validations
{
    public class FormulaValidator : ValidationAttribute
    {
        private bool isFormulaValid(string formula)
        {
            if (string.IsNullOrEmpty(formula))
                return false;

            if (string.IsNullOrWhiteSpace(formula))
                return false;

            var result = formula.ToCharArray().Any(c => char.IsLetter(c) && c != 'a');
            if (result)
                return false;

            result = Regex.IsMatch(formula, Patterns.FolrmulaPattern);
            if (!result)
                return false;

            var reg = new Regex(Patterns.ParenthesesPattern, RegexOptions.IgnorePatternWhitespace);
            result = reg.IsMatch(formula);
            if (!result)
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
