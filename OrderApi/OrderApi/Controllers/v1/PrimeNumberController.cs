using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WolfgangOfner.MicroserviceDemo.PrimeNumber;

namespace OrderApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class PrimeNumberController : ControllerBase
    {
        /// <summary>
        /// Action to calculate the n-Th prime number.
        /// </summary>
        /// <param name="nThPrimeNumber">Calculate the n-th prime number</param>
        /// <returns>Returns the 'nThPrimeNumber' prime number.</returns>
        /// <response code="200">Returned if the prime number was calculated.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public long Index(int nThPrimeNumber)
        {
            return PrimeNumber.FindNthPrimeNumber(nThPrimeNumber);
        }
    }
}