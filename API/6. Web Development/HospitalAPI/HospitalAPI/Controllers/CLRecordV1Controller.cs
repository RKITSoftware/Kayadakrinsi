using System.Collections.Generic;
using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.Models;


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
        #region Public Members

        /// <summary>
        /// List of records
        /// </summary>
        public List<RCD01> records = RCD01.GetRecords();

        #endregion

        #region Public Methods

        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        public IHttpActionResult GetFewRecords()
        {
            return Ok(records.FindAll(r => r.D01F01 <= 3));
        }

        [BasicAuthorizationAttribute(Roles = "SuperAdmin,Admin")]
        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        public IHttpActionResult GetMoreRecords()
        {
            return Ok(records.FindAll(r => r.D01F01 <= 6));
        }

        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        public IHttpActionResult GetAllRecords()
        {
            return Ok(records);
        }

        #endregion
    }
}
