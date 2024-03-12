using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLHelperController : ApiController
    {
		#region Public Members

		/// <summary>
		/// Declares object of BLHelper class
		/// </summary>
		public BLHelper objBLHelper;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

        /// <summary>
        /// Declares object of class BLUser
        /// </summary>
        public BLUser objBLUser;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLHelperController()
		{
			objBLHelper = new BLHelper();
			objBLUser = new BLUser();
			stopwatch = Stopwatch.StartNew();
		}

		#endregion

		/// <summary>
		/// Displays helpers information
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager,helper")]
		[Route("api/CLHelper/GetHelpers")]
		public IHttpActionResult GetHelpers()
		{
			var data = objBLHelper.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
		}

		/// <summary>
		/// Downloads file of helper's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLHelper/GetHelpersFile")]
		public HttpResponseMessage GetHelpersFile()
		{
			return objBLHelper.Download();
		}

		/// <summary>
		/// Downloads file of doctor's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "helper")]
		[Route("api/CLHelper/GetMyFile")]
		public HttpResponseMessage GetMyFile()
		{
			var user = objBLUser.GetUser(ActionContext);
			return objBLHelper.DownloadMyFile(user);
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "helper")]
		[Route("api/CLHelper/WriteMyFile")]
		public IHttpActionResult WriteMyFile()
		{
			var user = objBLUser.GetUser(ActionContext);
			return Ok(objBLHelper.WriteMyFile(user));
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLHelper/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLHelper.WriteData());
		}

		/// <summary>
		/// Updates helper information
		/// </summary>
		/// <param name="objSTF02">Helper information to be updated</param>
		/// <returns>Appropriate message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLHelper/UpdateHelpers")]
		public IHttpActionResult UpdateHelpers(STF02 objSTF02)
		{
			return Ok(objBLHelper.Update(objSTF02));
		}
	}
}
