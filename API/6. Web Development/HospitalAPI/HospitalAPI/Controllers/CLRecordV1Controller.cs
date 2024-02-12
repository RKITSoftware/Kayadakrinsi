using System.Web;
using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.BusinesLogic;


namespace HospitalAPI.Controllers
{
    /// <summary>
    /// Custom cntroller for handling requests
    /// </summary>
    //[BLCacheFilter(TimeDuration = 100)]
    [BasicAuthenticationAttribute]
    public class CLRecordV1Controller : ApiController
    {
        /// <summary>
        /// Declares object of BLRecord class
        /// </summary>
        public BLRecord objBLRecord;

        /// <summary>
        /// Initializes object of BLRecord class
        /// </summary>
        public CLRecordV1Controller()
        {
            objBLRecord = new BLRecord();
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
            HttpContext.Current.Cache.Insert("SomeRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

            return Ok(data);
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
            HttpContext.Current.Cache.Insert("MoreRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

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
            HttpContext.Current.Cache.Insert("AllRecords v1", data, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);

            return Ok(data);
        }

        #endregion
    }
}
