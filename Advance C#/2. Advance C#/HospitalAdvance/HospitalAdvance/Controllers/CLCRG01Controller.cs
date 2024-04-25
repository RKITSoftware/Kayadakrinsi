using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Handles HTTP request for operations on charges
    /// </summary>
    [BearerAuthentication]
    [RoutePrefix("api/CLCRG01")]
    public class CLCRG01Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of class BLCRG01Handler
        /// </summary>
        public BLCRG01Handler objBLCRG01Handler;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public Stopwatch stopwatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes objects
        /// </summary>
        public CLCRG01Controller()
        {
            objBLCRG01Handler = new BLCRG01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays charges
        /// </summary>
        /// <returns>List of charges</returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("GetCharges")]
        public IHttpActionResult GetCharges()
        {
            Response response = new Response();

            response = objBLCRG01Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of charges data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        [Route("GetChargesFile")]
        public HttpResponseMessage GetChargesFile()
        {
            return objBLCRG01Handler.Download();
        }

        /// <summary>
        /// Adds new charge
        /// </summary>
        /// <param name="objCRG01">Charge to be added</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Route("AddCharge")]
        public IHttpActionResult AddCharge(DTOCRG01 objDTOCRG01)
        {
            objBLCRG01Handler.ObjOperations = enmOperations.I;

            objBLCRG01Handler.PreSave(objDTOCRG01);

            Response response = objBLCRG01Handler.Validation();

            if (!response.isError)
            {
                response = objBLCRG01Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates charge
        /// </summary>
        /// <param name="objCRG01">Charge to be updated</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "Manager")]
        [Route("EditCharge")]
        public IHttpActionResult EditCharge(DTOCRG01 objDTOCRG01)
        {
            objBLCRG01Handler.ObjOperations = enmOperations.U;

            objBLCRG01Handler.PreSave(objDTOCRG01);

            Response response = objBLCRG01Handler.Validation();

            if (!response.isError)
            {
                response = objBLCRG01Handler.Save();
            }

            return Ok(response);
        }

        #endregion

    }
}
