using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLDoctorController : ApiController
    {
		#region Public Members

		/// <summary>
		/// Declares object of BLDoctor class
		/// </summary>
		public BLDoctor objBLDoctor;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes object of BLDoctor class
		/// </summary>
		public CLDoctorController()
		{
			objBLDoctor = new BLDoctor();
			stopwatch = Stopwatch.StartNew();
		}

		#endregion

		/// <summary>
		/// Displays doctors information
		/// </summary>
		/// <returns>List of doctors</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "admin,doctor")]
		[Route("api/CLDoctor/GetDoctors")]
		public IHttpActionResult GetDoctors()
		{
			var data = objBLDoctor.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
		}

		/// <summary>
		/// Downloads file of doctor's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLDoctor/GetFile")]
		public HttpResponseMessage GetFile()
		{
			return objBLDoctor.Download();
		}

		/// <summary>
		/// Downloads file of doctor's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "doctor")]
		[Route("api/CLDoctor/GetMyFile")]
		public HttpResponseMessage GetMyFile()
		{
			var user = BLUser.GetUser(ActionContext);
			return objBLDoctor.DownloadMyFile(user);
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "doctor")]
		[Route("api/CLDoctor/WriteMyFile")]
		public IHttpActionResult WriteMyFile()
		{
			var user = BLUser.GetUser(ActionContext);
			return Ok(objBLDoctor.WriteMyFile(user));
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLDoctor/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLDoctor.WriteData());
		}

		/// <summary>
		/// Updates doctor information
		/// </summary>
		/// <param name="objSTF01">Doctor information to be updated</param>
		/// <returns>Appropriate message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "admin")]
		[Route("api/CLDoctor/UpdateDoctors")]
		public IHttpActionResult UpdateDoctors(STF01 objSTF01) 
		{ 
			return Ok(objBLDoctor.Update(objSTF01));
		}
	}
}
