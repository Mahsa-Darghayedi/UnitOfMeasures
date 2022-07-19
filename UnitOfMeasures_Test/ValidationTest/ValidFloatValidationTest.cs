using UnitOfMeasures.Application.Models.Validations;
using Xunit;

namespace UnitOfMeasures_Test.ValidationTest
{
    public class ValidFloatValidationTest
    {

        [Theory]
        [InlineData(5.6)]
        [InlineData(5)]
        [InlineData(55.66)]
        [InlineData(0.6)]
        [InlineData(5.06)]
        [InlineData(50.6)]
        public void ValidDouble_Success(double value)
        {
            ValidDoubleValidator validator = new ValidDoubleValidator();
            Assert.True(validator.IsValid(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1")]
        [InlineData("a")]
        [InlineData("1.2")]
        public void ValidFloat_Failed(string value)
        {
            ValidDoubleValidator validator = new ValidDoubleValidator();
            Assert.False(validator.IsValid(value));
        }
    }
}
