using UnitOfMeasures.Application.Models.Validations;
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


        [Theory]
        [InlineData("aa")]
        [InlineData("a+6a")]
        [InlineData("a&a")]
        [InlineData("4%a")]
        [InlineData("a+++6a")]
        [InlineData("a++6a-")]
        [InlineData("a++6a")]
        [InlineData("a+b+6")]
        [InlineData("((a+b+6)")]
        [InlineData("()")]
        [InlineData("(())")]
        [InlineData("((a.6))")]
        [InlineData("5.5(a*a/2-6.5)")]
        [InlineData("(4.5)()")]
        [InlineData("(4.5)(a)")]
        public void FormulaTest_Failure_InvalidFormula(string value)
        {
            var result = new FormulaValidator().IsValid(value);
            Assert.False(result);
        }

        [Theory]
        [InlineData("(a*a/2-6.5)")]
        [InlineData("(4.5)")]
        [InlineData("5.5+(a*a/2-6.5)")]
        [InlineData("a+273.15")]
        [InlineData("(a*9/5)+32")]
        public void FormulaTest_Failure_InvalidFormulaP(string value)
        {
            var result = new FormulaValidator().IsValid(value);
            Assert.True(result);
        }
    }
}
