using System.Collections.Generic;
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

        /// <summary>
        /// Handles get request for admin
        /// </summary>
        /// <returns>List of admins</returns>
        [HttpGet]
        [Route("api/CLData/GetAdmins")]
        public IHttpActionResult GetAdmins()
        {
            return Ok(BLAdmin.admins);
        }

        #endregion
    }
}
