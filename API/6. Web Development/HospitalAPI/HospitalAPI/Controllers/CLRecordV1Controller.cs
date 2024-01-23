using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.BusinesLogic;


namespace HospitalAPI.Controllers
{
    //// To enable croos origin server request 
    //[EnableCors(origins: "https://localhost:44380", headers: "*", methods: "*")]

    [BLCacheFilter(TimeDuration = 100)]
    [AuthenticationAttribute]

    /// <summary>
    /// Custom cntroller for handling requests
    /// </summary>
    public class CLRecordV1Controller : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        public IHttpActionResult GetFewRecords()
        {
            return Ok(BLRecord.GetSomeRecords());
        }

        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin,Admin")]
        public IHttpActionResult GetMoreRecords()
        {
                return Ok(BLRecord.GetMoreRecords());
        }

        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public IHttpActionResult GetAllRecords()
        {
            return Ok(BLRecord.lstRCD01);
        }

        #endregion
    }
}
