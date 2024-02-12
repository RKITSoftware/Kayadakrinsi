using System.Web.Http;
using TpesClass.BusinessLogic;
using TpesClass.Models;

namespace TpesClass.Controllers
{
    /// <summary>
    /// Custom controller for handling request users and admins requests.
    /// </summary>
    public partial class CLDataController : ApiController
    {
        #region Public Methods

        [HttpGet]
        [Route("api/CLData/GetUsers")]
        /// <summary>
        /// Hadles get request for user data
        /// </summary>
        /// <returns>List of users</returns>
        public IHttpActionResult GetUsers()
        {
            return Ok(BLUser.users);
        }

        [HttpPost]
        [Route("api/CLData/AddUser")]
        /// <summary>
        /// Handles post request for adding new user
        /// </summary>
        /// <param name="id">id of new user</param>
        /// <param name="newUser">Details of new user</param>
        /// <returns>List of users</returns>
        public IHttpActionResult AddUser(USR01 newUser)
        {
            var user = BLUser.users.Find(u => u.R01F01 == newUser.R01F01);
            if (user == null)
            {
                BLUser.users.Add(newUser);
                return Ok(BLUser.users);
            }
            return BadRequest();
        }

        #endregion
    }
}
