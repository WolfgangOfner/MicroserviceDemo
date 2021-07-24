using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        /// <summary>
        ///     Action returns an HTTP 500 error code.
        /// </summary>
        /// <returns>Returns an HTTP 500 error code.</returns>
        /// <response code="500">Always returns an HTTP 500 error code.</response>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public ActionResult Exception()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}