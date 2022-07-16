using UnitOfMeasures.Application.Validations;
using Xunit;
namespace UnitOfMeasures_Test.ValidationTest
{
    public class FormulaValidationTest
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void FormullaTest_Failure_FormulaNullOrEmpty(string value)
        {
            var result = new FormulaValidator().IsValid(value);
            Assert.False(result);
        }


        [Theory]
        [InlineData("ba")]
        [InlineData("b")]
        [InlineData("bbaa")]
        public void FormullaTest_Failure_LetterIsNotA(string value)
        {
            var result = new FormulaValidator().IsValid(value);
            Assert.False(result);
        }
    }
}
