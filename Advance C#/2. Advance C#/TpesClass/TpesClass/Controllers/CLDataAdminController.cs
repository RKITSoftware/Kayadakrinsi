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
        /// List of admins
        /// </summary>
        static List<ADM01> admins = ADM01.GetData();

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles get request for admin
        /// </summary>
        /// <returns>List of admins</returns>
        [HttpGet]
        [Route("api/CLData/GetAdmins")]
        public IHttpActionResult GetAdmins()
        {
            return Ok(admins);
        }

        #endregion
    }
}
