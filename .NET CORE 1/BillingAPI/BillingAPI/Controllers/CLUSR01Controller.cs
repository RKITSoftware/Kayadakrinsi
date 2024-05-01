using BillingAPI.BusinessLogic;
using BillingAPI.Models;
using BillingAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    /// <summary>
    /// Handles http request for user operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of BLUSR01 class
        /// </summary>
        public BLUSR01 objBLUSR01 = new BLUSR01();

        /// <summary>
        /// Retrives all users data
        /// </summary>
        /// <returns>Response containing all users data</returns>
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            Response response = new Response();

            response = objBLUSR01.Select();

            return Ok(response);
        }

        /// <summary>
        /// Adds user
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(DTOUSR01 objDTOUSR01)
        {
            objBLUSR01.Operations = Enums.enmOperations.I;

            objBLUSR01.PreSave(objDTOUSR01);

            Response response = objBLUSR01.Validation();

            if (!response.isError)
            {
                response = objBLUSR01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("EditUser")]
        public IActionResult EditUser(DTOUSR01 objDTOUSR01)
        {
            objBLUSR01.Operations = Enums.enmOperations.U;

            objBLUSR01.PreSave(objDTOUSR01);

            Response response = objBLUSR01.Validation();

            if (!response.isError)
            {
                response = objBLUSR01.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes user whoose id is given
        /// </summary>
        /// <param name="id">Id of user to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            Response response = new Response();

            response = objBLUSR01.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLUSR01.Delete(id);
            }

            return Ok(response);
        }

    }
}
