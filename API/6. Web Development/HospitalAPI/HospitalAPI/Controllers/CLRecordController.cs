using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalAPI.Auth;
using System.Web.Security;
using HospitalAPI.Models;
using System.Web.Http.Cors;

namespace HospitalAPI.Controllers
{
    //// To enable croos origin server request 
    //[EnableCors(origins: "https://localhost:44380", headers: "*", methods: "*")]

    [BLCacheFilter(TimeDuration = 100)]
    [AuthenticationAttribute]

    /// <summary>
    /// Custom cntroller for handling requests
    /// </summary>
    public class CLRecordController : ApiController
    {
        #region Public Members

        /// <summary>
        /// List of records
        /// </summary>
        public List<RCD01> records = RCD01.GetRecords();

        #endregion

        #region Public Methods

        [Route("GetFewRecords")]
        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        public HttpResponseMessage GetFewRecords()
        {
            return Request.CreateResponse(HttpStatusCode.OK, records.Where(c => c.D01F01 <= 3));
        }

        [Route("GetMoreRecords")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        public HttpResponseMessage GetMoreRecords()
        {
            return Request.CreateResponse(HttpStatusCode.OK, records.Where(c => c.D01F01 <= 6));
        }

        [Route("GetAllRecords")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        public HttpResponseMessage GetAllRecords()
        {
            return Request.CreateResponse(HttpStatusCode.OK, RCD01.GetRecords());
        }

        #endregion
    }
}
