using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.BusinesLogic;
using HospitalAPI.Filters;

namespace HospitalAPI.Controllers
{
    /// <summary>
    /// Custom cntroller for handling requests
    /// </summary>
    [CustomExceptionFilterAttribute]
    [BasicAuthenticationAttribute]
    public class CLRecordV1Controller : ApiController
    {
        /// <summary>
        /// Declares object of BLRecord class
        /// </summary>
        public BLRecord objBLRecord;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public static Stopwatch stopwatch;

        /// <summary>
        /// Object of Cache class
        /// </summary>
        private Cache _objCache = new Cache();

        /// <summary>
        /// Initializes object of BLRecord class
        /// </summary>
        public CLRecordV1Controller()
        {
            objBLRecord = new BLRecord();
            stopwatch = Stopwatch.StartNew();
        }

        #region Public Methods

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        public IHttpActionResult GetFewRecords()
        {
            var data = objBLRecord.GetSomeRecords();
            _objCache.Insert("SomeRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            // HttpContext.Current.Session.Clear();

            // Return data (first from cache else from list)
            return Ok(data);

            // returns data if it exist in cache only else returns null 
            // return Ok(_objCache.Get("SomeRecords v1"));
        }

        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin,Admin")]
        public IHttpActionResult GetMoreRecords()
        {
            var data = objBLRecord.GetMoreRecords();
            _objCache.Insert("MoreRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(data);
        }

        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public IHttpActionResult GetAllRecords()
        {
            var data = BLRecord.lstRCD01;
            _objCache.Insert("AllRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(data);
        }

        /// <summary>
        /// Displays cache data if any
        /// </summary>
        /// <returns>cache data</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public IHttpActionResult GetCache()
        {
            var SomeRecords = _objCache.Get("SomeRecords v1");
            var MoreRecords = _objCache.Get("MoreRecords v1");
            var AllRecords = _objCache.Get("AllRecords v1");
            return Ok(new { SomeRecords, MoreRecords, AllRecords });
        }

        #endregion

    }
}
