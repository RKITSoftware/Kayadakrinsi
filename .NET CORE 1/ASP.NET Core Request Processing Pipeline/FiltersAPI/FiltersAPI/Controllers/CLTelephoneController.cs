using FiltersAPI.BusinessLogic;
using FiltersAPI.Filters;
using FiltersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiltersAPI.Controllers
{
    /// <summary>
    /// Telephone controller
    /// </summary>
    [Route("FiltersAPI/[controller]")]
    [BasicAuthenticationFilter]
    [ServiceFilter(typeof(ResourceFilter))]
    [ResultFilter]
    [AsyncResultFilter]
    [ApiController]
    public class CLTelephoneController : ControllerBase
    {
        /// <summary>
        /// Declares object of class BLTelephone
        /// </summary>
        public BLTelephone objBLTelephone;

        /// <summary>
        /// Initializes object of class BLTelephone
        /// </summary>
        public CLTelephoneController()
        {
            objBLTelephone = new BLTelephone();
        }

        /// <summary>
        /// Handles request for get telephone records
        /// </summary>
        /// <returns>List of telephone records</returns>
        [HttpGet]
        [Route("GetRecords")]
        public IActionResult GetRecords()
        { 
            return Ok(BLTelephone.lstTEL01);
        }

        /// <summary>
        /// Handles request for getting single telephone record by id
        /// </summary>
        /// <param name="id">Id of telephone records user wants to get</param>
        /// <returns>Telephone record if exist, Bad request otherwise</returns>
        [HttpGet]
        [Route("GetRecordById")]
        public IActionResult GetRecordById(int id)
        {
            var result =  objBLTelephone.GetRecordById(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Handles requeat for adding telephone record to the list
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [Route("AddRecord")]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult AddRecord(TEL01 objTEL01)
        {
            if(objBLTelephone.Validation(objTEL01))
            {
                return Ok(objBLTelephone.AddRecord(objTEL01));
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        /// <summary>
        /// Handles requeat for updating telephone record into the list
        /// </summary>
        /// <param name="objTEL01">Object of class TEL01</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [Route("UpdateRecord")]
        public IActionResult UpdateRecord(TEL01 objTEL01)
        {
            if (objBLTelephone.validationUpdate(objTEL01))
            {
                return Ok(objBLTelephone.UpdateRecord(objTEL01));
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        /// <summary>
        /// Handles request for deleting telephone record from the list
        /// </summary>
        /// <param name="id">Id of telephone records user wants to delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteRecord")]
        public IActionResult DeleteRecord(int id)
        {
            var result = objBLTelephone.DeleteRecord(id);

            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }

}

