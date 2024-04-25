using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Handles methods to preform operations related to user
    /// </summary>
    [RoutePrefix("api/CLUSR01")]
    public class CLUSR01Controller : ApiController
    {
        #region Public Members

        /// <summary>
        /// Declares object of BLUSR01Handler class
        /// </summary>
        public BLUSR01Handler objBLUSR01Handler;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public Stopwatch stopwatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLUSR01Controller()
        {
            objBLUSR01Handler = new BLUSR01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles get reuest for getting user data
        /// </summary>
        /// <returns>All data from table USR01</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            Response response = objBLUSR01Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Retrives cache
        /// </summary>
        /// <returns>All data in cache</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetCache")]
        public IHttpActionResult GetCache()
        {
            Dictionary<string, object> response = objBLUSR01Handler.GetCache();

            return Ok(response);
        }


        #endregion

    }

}
