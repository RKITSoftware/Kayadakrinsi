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
        /// Gets users data
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("api/CLUsers/GetUsers")]
        public IHttpActionResult GetUsers() {
            return Ok(BLUsers.Users());
        }

        /// <summary>
        /// Get admin data
        /// </summary>
        /// <returns>List of admins</returns>
        [HttpGet]
        [Route("api/CLUsers/GetAdmins")]
        public IHttpActionResult GetAdmins()
        {
            return Ok(BLUsers.Admins());
        }
        
        /// <summary>
        /// Get data of all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        [Route("api/CLUsers/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(BLUsers.AllUsers());
        }
    }
}