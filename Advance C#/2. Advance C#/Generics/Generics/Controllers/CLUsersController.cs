using System.Web.Http;
using Generics.BusinessLogic;

namespace Generics.Controllers
{
    /// <summary>
    /// Handles HTTp request of users
    /// </summary>
    public class CLUsersController : ApiController
    {
        /// <summary>
        /// Creates object of class BLUsers
        /// </summary>
        public BLUsers objBLUsers = new BLUsers();

        /// <summary>
        /// Gets users data
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("api/CLUsers/GetUsers")]
        public IHttpActionResult GetUsers() {
            return Ok(objBLUsers.Users());
        }

        /// <summary>
        /// Get admin data
        /// </summary>
        /// <returns>List of admins</returns>
        [HttpGet]
        [Route("api/CLUsers/GetAdmins")]
        public IHttpActionResult GetAdmins()
        {
            return Ok(objBLUsers.Admins());
        }
        
        /// <summary>
        /// Get data of all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        [Route("api/CLUsers/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(objBLUsers.AllUsers());
        }
    }
}