using Logging.BusinessLogic;
using Logging.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers
{
    /// <summary>
    /// Handles HTTP requests for Stationary items
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLStationaryController : ControllerBase
    {
        /// <summary>
        /// Declares logger of type ILogger
        /// </summary>
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        
        /// <summary>
        /// Declares object of BLStationary class
        /// </summary>
        public BLStationary objBLStationary;

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLStationaryController(ILogger<CLStationaryController> logger)
        {
            _logger = logger;
            objBLStationary = new BLStationary();
        }

        /// <summary>
        /// Retrives stationary items
        /// </summary>
        /// <returns>Object of class RES01</returns>
        [HttpGet]
        [Route("GetItems")]
        public IActionResult GetItems()
        {
            return Ok(objBLStationary.GetItems());
        }

        /// <summary>
        /// Adds new stationary item into the list
        /// </summary>
        /// <param name="objSTA01">Object of STA01 class</param>
        /// <returns>Object of class RES01</returns>
        [HttpPost]
        [Route("AddItem")]
        public IActionResult AddItem(STA01 objSTA01)
        {
            BLStationary.objSTA01 = objSTA01;

            if (objBLStationary.Validation())
            {
                return Ok(objBLStationary.AddItem());
            }

            _logger.LogWarning(String.Format(@"{0} | Invalid data", ControllerContext.ActionDescriptor.ActionName));

            RES01 objRES01 = new RES01 { isError=false,message="Invalid data"};

            return Ok(objRES01);

        }

        /// <summary>
        /// Updates stationary item into the list
        /// </summary>
        /// <param name="objSTA01">Object of STA01 class</param>
        /// <returns>Object of class RES01</returns>
        [HttpPut]
        [Route("EditItem")]
        public IActionResult EditItem(STA01 objSTA01)
        {
            BLStationary.objSTA01 = objSTA01;

            if (objBLStationary.Validation())
            {
                return Ok(objBLStationary.UpdateItem());
            }

            _logger.LogWarning(String.Format(@"{0} | Invalid data", ControllerContext.ActionDescriptor.ActionName));

            RES01 objRES01 = new RES01 { isError = false, message = "Invalid data" };

            return Ok(objRES01);
        }

        /// <summary>
        /// Deletes stationary item from the list
        /// </summary>
        /// <param name="id">Id of stationary item to be delete</param>
        /// <returns>Object of class RES01</returns>
        [HttpDelete]
        [Route("DeleteItem")]
        public IActionResult DeleteItem(int id)
        {
            return Ok(objBLStationary.DelteteItem(id));
        }
    }
}
