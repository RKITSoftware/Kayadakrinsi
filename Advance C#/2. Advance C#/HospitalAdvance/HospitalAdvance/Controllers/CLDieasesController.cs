using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
	public class CLDieasesController : ApiController
    {

		#region Public Members

		/// <summary>
		/// Declares object of BLDieases class
		/// </summary>
		public BLDieases objBLDieases;

		/// <summary>
		/// Declares object of Stopwatch class
		/// </summary>
		public static Stopwatch stopwatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes objects
		/// </summary>
		public CLDieasesController()
		{
			objBLDieases = new BLDieases();
			stopwatch = Stopwatch.StartNew();
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays dieases 
        /// </summary>
        /// <returns>List of dieases</returns>
        [HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLDieases/GetDieasess")]
		public IHttpActionResult GetDieasess()
		{
			var data = objBLDieases.Select();

			stopwatch.Stop();
			long responseTime = stopwatch.ElapsedTicks;

			HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

			return Ok(data);
		}

		/// <summary>
		/// Downloads file of dieases data
		/// </summary>
		/// <returns>Downloaded text file</returns>
		[HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLDieases/GetDieasesFile")]
		public HttpResponseMessage GetDieasesFile()
		{
			return objBLDieases.Download();
		}

		/// <summary>
		/// Adds new dieases
		/// </summary>
		/// <param name="objDIS01">Dieases to be added</param>
		/// <returns>Appropriate message</returns>
		[HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLDieases/AddDieases")]
		public IHttpActionResult AddDieases(DIS01 objDIS01)
		{
			if (objBLDieases.validation(objDIS01))
			{
				return Ok(objBLDieases.Insert(objDIS01));
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
		[Route("api/CLDieases/WriteFile")]
		public IHttpActionResult WriteFile()
		{
			return Ok(objBLDieases.WriteData());
		}

		/// <summary>
		/// Updates dieases
		/// </summary>
		/// <param name="objDIS01">Dieases to be updated</param>
		/// <returns>Appropriate message</returns>
		[HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "Manager")]
		[Route("api/CLDieases/EditDieases")]
		public IHttpActionResult EditDieases(DIS01 objDIS01)
		{
            if (objBLDieases.validation(objDIS01))
            {
				return Ok(objBLDieases.Update(objDIS01));
            }
            return BadRequest("Invalid data");
		}

        #endregion

    }
}
