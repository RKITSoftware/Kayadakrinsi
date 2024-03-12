using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using HospitalAPI.Auth;
using HospitalAPI.BusinesLogic;
using HospitalAPI.Filters;
using HospitalAPI.Models;

namespace HospitalAPI.Controllers
{
	/// <summary>
	/// Custom controller for user requests
	/// </summary>
	[CustomExceptionFilterAttribute]
	public class CLRecordV2Controller : ApiController
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
        /// Initializes object of BLRecord and Stopwatch class
        /// </summary>
        public CLRecordV2Controller()
        {
            objBLRecord = new BLRecord();
            stopwatch = Stopwatch.StartNew();
        }

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
		/// CORS implementation
		/// </summary>
		/// <returns>Message</returns>
		[HttpGet]
		[Route("api/v2/getCORS")]
		public IHttpActionResult GetCORS()
		{
			return Ok("CORS enable worked");
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
            List<RCD01> lstData;
            if (_objCache["SomeRecords v2"] != null)
            {
                lstData = (List<RCD01>)_objCache.Get("SomeRecords v2");
            }
            else
            {
				lstData = objBLRecord.GetSomeRecords();
				_objCache.Insert("SomeRecords v2", lstData, null, System.DateTime.Now.AddMinutes(20),System.TimeSpan.Zero);
            }

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(lstData);
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
            Stopwatch stopwatch = Stopwatch.StartNew();

			List<RCD01> lstData;
			if (_objCache["MoreRecords v2"] != null)
			{
				lstData = (List<RCD01>)_objCache.Get("MoreRecords v2");
			}
			else
			{
				lstData = objBLRecord.GetSomeRecords();
				_objCache.Insert("MoreRecords v2", lstData, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);
			}

			stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());
            
            return Ok(lstData);
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
            Stopwatch stopwatch = Stopwatch.StartNew();

			List<RCD01> lstData;
			if (_objCache["AllRecords v2"] != null)
			{
				lstData = (List<RCD01>)_objCache.Get("AllRecords v2");
			}
			else
			{
				lstData = BLRecord.lstRCD01;
				_objCache.Insert("AllRecords v2", lstData, null, System.DateTime.Now.AddMinutes(20), System.TimeSpan.Zero);
			}
			
            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(lstData);
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
            var SomeRecords = _objCache.Get("SomeRecords v2");
            var MoreRecords = _objCache.Get("MoreRecords v2");
            var AllRecords = _objCache.Get("AllRecords v2");
            return Ok(new { SomeRecords, MoreRecords, AllRecords});
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
    }
}
