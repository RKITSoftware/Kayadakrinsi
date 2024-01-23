using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HTTP_Caching.Controllers
{
    [BLCacheFilter(TimeDuration = 100)]
    /// <summary>
    /// Defines custom controller
    /// </summary>
    public class CLDataController : ApiController
    {
        [AllowAnonymous] // Anyone can access this cache
        [Route("GetData")] // Route is changed to GetData instead of api/["controller"]
        [BLCacheFilter(TimeDuration = 100)] // Using custom cache filter made previously with it's parameter

        #region Public Methods

        /// <summary>
        /// Returns dummy data defined in it with the help of dictionary
        /// </summary>
        public IHttpActionResult getData()
        {
            return Ok(BLData.Data());
        }

        #endregion
    }
}
