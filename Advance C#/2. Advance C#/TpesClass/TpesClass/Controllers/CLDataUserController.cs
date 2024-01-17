using System.Collections.Generic;
using System.Web.Http;
using TpesClass.Models;

namespace TpesClass.Controllers
{
    /// <summary>
    /// Custom controller for handling request users and admins requests.
    /// </summary>
    public partial class CLDataController : ApiController
    {
        #region Static Members

        /// <summary>
        /// List of users
        /// </summary>
        static List<USR01> users = USR01.GetData();

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("api/CLData/GetUsers")]
        /// <summary>
        /// Hadles get request for user data
        /// </summary>
        /// <returns>List of users</returns>
        public IHttpActionResult GetUsers()
        {
            return Ok(users);
        }

        [HttpPost]
        [Route("api/CLData/AddUser")]
        /// <summary>
        /// Handles post request for adding new user
        /// </summary>
        /// <param name="id">id of new user</param>
        /// <param name="newUser">Details of new user</param>
        /// <returns>List of users</returns>
        public IHttpActionResult AddUser(int id, USR01 newUser)
        {
            var user = users.Find(u => u.R01F01 == id);
            if (user == null)
            {
                users.Add(newUser);
                return Ok(users);
            }
            return BadRequest();
        }

        #endregion
    }
}
