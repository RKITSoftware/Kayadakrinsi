using FiltersAPI.BusinessLogic;
using FiltersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiltersAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("FiltersAPI/[controller]")]
    [ApiController]
    public class CLUserController : ControllerBase
    {
        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLUser objBLUser;

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLUserController()
        {
            objBLUser = new BLUser();
        }

        /// <summary>
        /// Handles request for get user
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(BLUser.lstUSR01);
        }

        /// <summary>
        /// Handles request for add user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(USR01 objUSR01)
        {
            if (objBLUser.Validation(objUSR01))
            {
                return Ok(objBLUser.AddUser(objUSR01));
            }
            else
            {
                return BadRequest("Invalid data!");
            }
        }
    }
}