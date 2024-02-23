using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLPatientController : ApiController
    {
		#region Public Members

		/// <summary>
		/// Declares object of BLPatient class
		/// </summary>
		public BLPatient objBLPatient;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes objects
		/// </summary>
		public CLPatientController()
		{
			objBLPatient = new BLPatient();
			stopwatch = Stopwatch.StartNew();
		}

		#endregion

		/// <summary>
		/// Displays patient information
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "admin,patient")]
		[Route("api/CLPatient/GetPatients")]
		public IHttpActionResult GetPatients()
		{
			var data = objBLPatient.Select();
			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
		}

		/// <summary>
		/// Downloads file of patient's data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLPatient/GetPatientsFile")]
		public HttpResponseMessage GetPatientsFile()
		{
			return objBLPatient.Download();
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLPatient/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLPatient.WriteData());
		}

		/// <summary>
		/// Updates patient information
		/// </summary>
		/// <param name="objPTN01">Patient information to be updated</param>
		/// <returns>Appropriate message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "admin")]
		[Route("api/CLPatient/UpdatePatients")]
		public IHttpActionResult UpdatePatients(PTN01 objPTN01)
		{
			return Ok(objBLPatient.Update(objPTN01));
		}
	}
}
