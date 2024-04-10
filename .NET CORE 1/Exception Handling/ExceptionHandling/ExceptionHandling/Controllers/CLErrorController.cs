using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandling.Controllers
{
    /// <summary>
    /// Handles user requests
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CLErrorController : ControllerBase
    {
        /// <summary>
        /// Tries to generate divide by zero exception
        /// </summary>
        /// <returns>Result of division operation</returns>
        [HttpGet]
        [Route("DivideByZero")]
        public IActionResult DivideByZero()
        {
            int num = 10;
            int result = num / 0;
            return Ok(result);
        }

        /// <summary>
        /// Tries to get element at 7th position from list
        /// </summary>
        /// <returns>List's element at 7th position</returns>
        [HttpGet]
        [Route("OutOfBound")]
        public IActionResult OutOfBound()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6};
            return Ok(numbers[7]);
        }

        /// <summary>
        /// Tries to get double value from string
        /// </summary>
        /// <returns>Double value of string</returns>
        [HttpGet]
        [Route("FormatException")]
        public IActionResult FormatException()
        {
            var result = "String";
            return Ok(Convert.ToDouble(result));
        }

        /// <summary>
        /// Not implemented method
        /// </summary>
        /// <returns>NotImplemented exception in DeveloperExceptionPage</returns>
        /// <exception cref="NotImplementedException">NotImplementedException</exception>
        [HttpGet]
        [Route("NotImplemented")]
        public IActionResult NotImplemented() 
        {
            throw new NotImplementedException();
        }
    }
}
            