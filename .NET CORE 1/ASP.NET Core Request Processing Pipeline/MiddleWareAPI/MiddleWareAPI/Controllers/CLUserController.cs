using Microsoft.AspNetCore.Mvc;
using MiddleWareAPI.BusinessLogic;
using MiddleWareAPI.Models;

namespace MiddleWareAPI.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
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
        [Route("MiddleWareAPI/CLUser/GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(BLUser.lstUser);
        }

        /// <summary>
        /// Handles request for add user2+
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("MiddleWareAPI/CLUser/AddUser")]
        public IActionResult AddUSer(string username,string password)
        {
                USR01 objUSR01 = objBLUser.PreSave(username, password);
                if(objBLUser.Validation(objUSR01))
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
