using System;
using CustomerApi.Validators.v1;
using FluentValidation.TestHelper;
using Xunit;

namespace CustomerApi.Test.Validator.v1
{
    public class UpdateCustomerModelValidatorTests
    {
        private readonly UpdateCustomerModelValidator _testee;

        public UpdateCustomerModelValidatorTests()
        {
            _testee = new UpdateCustomerModelValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void FirstName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string firstName)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.FirstName, firstName).WithErrorMessage("The first name must be at least 2 character long");
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void LastName_WhenShorterThanTwoCharacter_ShouldHaveValidationError(string lastName)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.LastName, lastName).WithErrorMessage("The last name must be at least 2 character long");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(151)]
        public void Age_WhenNegativeOrGreaterThan150_ShouldHaveValidationError(int age)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Age, age).WithErrorMessage("The minimum age is 0 and the maximum age is 150 years");
        }

        [Theory]
        [MemberData(nameof(BirthdayTestData.InvalidTestData), MemberType = typeof(BirthdayTestData))]
        public void Birthday_WhenLongerAgoThan150YearsOrInTheFutureOrNull_ShouldHaveValidationError(DateTime birthday)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Birthday, birthday)
                .WithErrorMessage("The birthday must not be longer ago than 150 years and can not be in the future");
        }

        [Theory]
        [MemberData(nameof(BirthdayTestData.ValidTestData), MemberType = typeof(BirthdayTestData))]
        public void Birthday_WhenBetween0And150_ShouldNotHaveValidationError(DateTime birthday)
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.Birthday, birthday);
        }

        [Theory]
        [InlineData(150)]
        [InlineData(0)]
        public void Age_WhenBetween0And150_ShouldNotHaveValidationError(int age)
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.Age, age);
        }

        [Fact]
        public void FirstName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.FirstName, "Ab");
        }

        [Fact]
        public void LastName_WhenLongerThanTwoCharacter_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.LastName, "Ab");
        }
    }
}