using System.Net;
using System.Text.Json;
using ActionMethods.BusinessLogic;
using ActionMethods.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionMethods.Controllers
{
    /// <summary>
    /// Handles http requests for COM01
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLActionController : ControllerBase
    {
        /// <summary>
        /// Declares object of class BLComputer
        /// </summary>
        private readonly BLComputer _objBLComputer;

        /// <summary>
        /// Initializes object of class BLComputer
        /// </summary>
        public CLActionController()
        {
            _objBLComputer = new BLComputer();
        }

        /// <summary>
        /// Tries to return async task of IActionResult type
        /// </summary>
        /// <returns>List of computers</returns>
        [HttpGet]
        [Route("GetAsyncTask")]
        public async Task<IActionResult> GetAsyncTask()
        {
            // Simulating asynchronous operation
            await Task.Delay(5000);

            return Ok(BLComputer.lstCOM01);

        }

        /// <summary>
        /// Tries to return HttpResponseMessage
        /// </summary>
        /// <returns>HttpResponseMessage with statuscode 200</returns>
        [HttpGet]
        [Route("GetHttpResponse")]
        public HttpResponseMessage GetHttpResponse()
        {
            string jsonContent = JsonSerializer.Serialize(BLComputer.lstCOM01);

            // Create a HttpResponseMessage with the JSON content
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            return response;

            //var result = new HttpResponseMessage(HttpStatusCode.OK);

            ////adss header field inside content doesn't show values
            ////result.Content = new StringContent(BLComputer.lstCOM01.ToString());

            //return result;

        }

        /// <summary>
        /// Tries to return ObjectResult
        /// </summary>
        /// <returns>List of computers</returns>
        [HttpGet]
        [Route("GetObjectResult")]
        public ObjectResult GetObjectResult()
        {
            return new ObjectResult(BLComputer.lstCOM01) { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Tries to return ContentResult
        /// </summary>
        /// <returns>List of computers</returns>
        [HttpGet]
        [Route("GetContentResult")]
        public ContentResult GetContentResult() 
        {
            string jsonContent = JsonSerializer.Serialize(BLComputer.lstCOM01);

            return Content(jsonContent, "application/json");

            //returns type of list only not list's content
            //return Content(BLComputer.lstCOM01.ToString(), "text/plain");
        }

        /// <summary>
        /// Tries to redirect to given url
        /// </summary>
        /// <returns>Response of url</returns>
        [HttpGet]
        [Route("GetRedirectResult")]
        public RedirectResult GetRedirectResult() 
        {
            return Redirect("/api/CLAction/GetComputers");
        }

        /// <summary>
        /// Tries to return JsonResult
        /// </summary>
        /// <returns>Json object of first computer in list</returns>
        [HttpGet]
        [Route("GetJsonResult")]
        public JsonResult GetJsonResult()
        {
            return new JsonResult(BLComputer.lstCOM01[0]);
        }

        /// <summary>
        /// Get computer's list
        /// </summary>
        /// <returns>List of computers</returns>
        [HttpGet]
        [Route("GetComputers")]
        public IActionResult GetComputers()
        {
            return Ok(BLComputer.lstCOM01);
        }

        /// <summary>
        /// Adds computer to the list
        /// </summary>
        /// <param name="objCOM01">Object of class COM01</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddComputer")]
        public IActionResult AddComputer([FromBody] COM01 objCOM01)
        {
            if (_objBLComputer.Validation(objCOM01))
            {
                return Ok(_objBLComputer.AddComputer(objCOM01));
            }
            return BadRequest("Invalid data");
        }

        /// <summary>
        /// Updates computer into the list
        /// </summary>
        /// <param name="objCOM01">Object of class COM01</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("UpdateComputer")]
        public IActionResult UpdateComputer([FromBody] COM01 objCOM01)
        {
            if (_objBLComputer.Validation(objCOM01))
            {
                var result = _objBLComputer.UpdateComputer(objCOM01);
                if (result == null)
                {
                    return NotFound("Object with given Guid not found");
                }
                else
                {
                    return Ok(result);
                }
            }
            return BadRequest("Invalid data");
        }

        /// <summary>
        /// Deletes computer from list
        /// </summary>
        /// <param name="guid">Guid of computer user wants delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteComputer")]
        public IActionResult DeleteComputer(Guid guid)
        {
            var result = _objBLComputer.DeteletComputer(guid);
            if (result == null)
            {
                return NotFound("Object with given Guid not found");
            }
            else
            {
                return Ok(result);
            }
        }

    }
}

