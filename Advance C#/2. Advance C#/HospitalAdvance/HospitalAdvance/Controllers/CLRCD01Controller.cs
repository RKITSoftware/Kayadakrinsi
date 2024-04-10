using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLRCD01Controller : ApiController
    {

		#region Public Members

		/// <summary>
		/// Declares object of BLRecord
		/// </summary>
		public BLRCD01 objBLRecord;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors
		
		/// <summary>
		/// Initializes objects
		/// </summary>
		public CLRCD01Controller()
        {
            objBLRecord = new BLRCD01();
			stopwatch = Stopwatch.StartNew();
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles ="Manager,Doctor,Helper,Patient")]
        [Route("api/CLRecord/GetRecords")]
        public IHttpActionResult GetRecords()
        {
			var data = objBLRecord.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
        }

		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager,Doctor,Helper,Patient")]
		[AllowAnonymous]
		[Route("api/CLRecord/GetDetailedRecords")]
		public IHttpActionResult GetDetailedRecords()
		{
			return Ok(objBLRecord.SelectAllDetails());
		}

		/// <summary>
		/// Downloads file of record's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager,Doctor,Helper,Patient")]
		[Route("api/CLRecord/GetFile")]
		public HttpResponseMessage GetFile()
		{
			return objBLRecord.Download();
		}
		
		/// <summary>
		/// Adds records
		/// </summary>
		/// <param name="objRCD01">Record to be added</param>
		/// <returns>Appropriate message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[AllowAnonymous]
		[Route("api/CLRecord/AddRecord")]
		public IHttpActionResult AddRecord(RCD01 objRCD01)
		{
			if(objBLRecord.preValidation(objRCD01))
			{
				objRCD01 = objBLRecord.preSave(objRCD01);
				if (objBLRecord.validation(objRCD01))
				{
					return Ok(objBLRecord.Insert(objRCD01));
				}
			}
			return BadRequest("Invalid data");
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLRecord/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLRecord.WriteData());
		}

        #endregion

    }
}
