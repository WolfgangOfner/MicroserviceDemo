using FluentAssertions;
using Xunit;

namespace WolfgangOfner.MicroserviceDemo.PrimeNumber.Test
{
    public class PrimeNumberTests
    {
        [Theory]
        [InlineData(3, 5)]
        [InlineData(5, 11)]
        [InlineData(50, 229)]
        public void FindNthPrimeNumber(int nThPrimeNumber, int expectedResult)
        {
            var result = PrimeNumber.FindNthPrimeNumber(nThPrimeNumber);

            result.Should().Be(expectedResult);
        }
    }
}