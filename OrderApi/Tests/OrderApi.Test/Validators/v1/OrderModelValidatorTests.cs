using FluentValidation.TestHelper;
using OrderApi.Validators.v1;
using Xunit;

namespace OrderApi.Test.Validators.v1
{
   public class OrderModelValidatorTests
    {
        private readonly OrderModelValidator _testee;

        public OrderModelValidatorTests()
        {
            _testee = new OrderModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("a")]
        public void CustomerFullName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string customerFullName)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.CustomerFullName, customerFullName).WithErrorMessage("The customer name must be at least 2 character long");
        }

        [Fact]
        public void CustomerFullName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.CustomerFullName, "Ab");
        }
    }
}
