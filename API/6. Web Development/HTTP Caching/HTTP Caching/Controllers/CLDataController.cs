using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HTTP_Caching.Controllers
{
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
            Dictionary<string, string> obj = new Dictionary<string, string>();
            obj.Add("1", "Punjab");
            obj.Add("2", "Assam");
            obj.Add("3", "UP");
            obj.Add("4", "AP");
            obj.Add("5", "J&K");
            obj.Add("6", "Odisha");
            obj.Add("7", "Delhi");
            obj.Add("9", "Karnataka");
            obj.Add("10", "Bangalore");
            obj.Add("21", "Rajesthan");
            obj.Add("31", "Jharkhand");
            obj.Add("41", "chennai");
            obj.Add("51", "jammu");
            obj.Add("61", "Bhubaneshwar");
            obj.Add("71", "Delhi");
            obj.Add("19", "Karnataka");

            return Ok(obj);
        }

        #endregion
    }
}
