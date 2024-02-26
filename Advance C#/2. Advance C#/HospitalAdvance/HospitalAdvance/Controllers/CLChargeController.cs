using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLChargeController : ApiController
    {
		#region Public Members

		/// <summary>
		/// Declares object of class BLCharge
		/// </summary>
		public BLCharge objBLCharge;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes objects
		/// </summary>
		public CLChargeController()
        {
			objBLCharge = new BLCharge();
			stopwatch = Stopwatch.StartNew();
		}

		#endregion

		/// <summary>
		/// Displays charges
		/// </summary>
		/// <returns>List of charges</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLCharge/GetCharges")]
        public IHttpActionResult GetCharges()
        {
			var data = objBLCharge.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
        }

		/// <summary>
		/// Downloads file of charges data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLCharge/GetChargesFile")]
		public HttpResponseMessage GetChargesFile()
		{
			return objBLCharge.Download();
		}

		/// <summary>
		/// Adds new charge
		/// </summary>
		/// <param name="objCRG01">Charge to be added</param>
		/// <returns>Appropriate message</returns>
		[HttpPost]
        [BearerAuthentication]
        [Authorize(Roles = "Admin")]
		[Route("api/CLCharge/AddCharge")]
		public IHttpActionResult AddCharge(CRG01 objCRG01)
		{
			return Ok(objBLCharge.Insert(objCRG01));
		}

		/// <summary>
		/// Write data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLCharge/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLCharge.WriteData());
		}

		/// <summary>
		/// Updates charge
		/// </summary>
		/// <param name="objCRG01">Charge to be updated</param>
		/// <returns>Appropriate message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
		[Route("api/CLCharge/EditCharge")]
		public IHttpActionResult EditCharge(CRG01 objCRG01)
		{
			return Ok(objBLCharge.Update(objCRG01));
		}

	}
}
