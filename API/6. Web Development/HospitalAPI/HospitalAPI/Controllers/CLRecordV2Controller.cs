using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.BusinesLogic;
using HospitalAPI.Models;

namespace HospitalAPI.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLRecordV2Controller : ApiController
    {
        /// <summary>
        /// Declares object of BLRecord class
        /// </summary>
        public BLRecord objBLRecord;

        /// <summary>
        /// Initializes object of BLRecord class
        /// </summary>
        public CLRecordV2Controller()
        {
            objBLRecord = new BLRecord();
        }

        #region Public Methods

        /// <summary>
        /// Used to get token
        /// </summary>
        /// <returns>Custom JWT token</returns>
        [HttpPost]
        [JWTAuthentication]
        public IHttpActionResult GetToken()
        {
            string[] usernamepassword = BLUser.GetUsernamePassword(Request);
            string username = usernamepassword[0];
            string password = usernamepassword[1];
            var userDetails = BLUser.GetUserDetails(username,password);
            if (userDetails != null)
            {
                return Ok(BLTokenManager.GenerateToken(username));
            }
            return BadRequest("Enter valid user details");
        }

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of records with id less than four</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = ("User"))]
        public IHttpActionResult GetSomeRecords()
        {
            var data = objBLRecord.GetSomeRecords();
            BLRecord.objCache.Insert("SomeRecords v2", data);
            return Ok(data);
        }

        /// <summary>
        /// Handles get request of admin, super admin
        /// </summary>
        /// <returns>List of records with id less than seven</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        public IHttpActionResult GetMoreRecords()
        {
            var data = objBLRecord.GetMoreRecords();
            BLRecord.objCache.Insert("MoreRecords v2", data);
            return Ok(data);
        }

        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of all records using HttpResponseMessage</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult GetAllRecords()
        {

            var data = BLRecord.lstRCD01;
            BLRecord.objCache.Insert("AllRecords v2", data);
            return Ok(data);
        }

        /// <summary>
        /// Displays cache data if any
        /// </summary>
        /// <returns>cache data</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult GetCache()
        {
            var SomeRecords = BLRecord.objCache.Get("SomeRecords v2");
            var MoreRecords = BLRecord.objCache.Get("MoreRecords v2");
            var AllRecords = BLRecord.objCache.Get("AllRecords v2");
            return Ok(new { SomeRecords, MoreRecords, AllRecords });
        }

        /// <summary>
        /// Hadles post request of super admin
        /// </summary>
        /// <param name="id">id of new record</param>
        /// <param name="newrecord">record to be add</param>
        /// <returns></returns>
        [HttpPost]
        [BearerAuthentication]
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        public IHttpActionResult AddRecord(RCD01 objRCD01)
        {
            return Ok(objBLRecord.AddRecord(objRCD01));
        }

        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="id">id of record to be edit</param>
        /// <param name="editedrecord">record with changes</param>
        /// <returns>List of records</returns>
        [HttpPut]
        [BearerAuthentication]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult EditRecord(RCD01 objRCD01)
        {
            return Ok(objBLRecord.EditRecord(objRCD01));
        }

        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of record to be delete</param>
        /// <returns>List of records</returns>
        [HttpDelete]
        [BearerAuthentication]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult DeleteRecord(int id)
        {
            return Ok(objBLRecord.DeleteRecord(id));
        }

        #endregion
    }
}
