using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Handles HTTP request for operations on dieases
    /// </summary>
    [RoutePrefix("api/CLDIS01")]
    public class CLDIS01Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of BLDIS01Handler class
        /// </summary>
        public BLDIS01Handler objBLDIS01Handler;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public Stopwatch stopwatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes objects
        /// </summary>
        public CLDIS01Controller()
        {
            objBLDIS01Handler = new BLDIS01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays dieases 
        /// </summary>
        /// <returns>List of dieases</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetDieasess")]
        public IHttpActionResult GetDieasess()
        {
            Response response = objBLDIS01Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of dieases response
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetDieasesFile")]
        public HttpResponseMessage GetDieasesFile()
        {
            return objBLDIS01Handler.Download();
        }

        /// <summary>
        /// Adds new dieases
        /// </summary>
        /// <param name="objDTODIS01">Object of DIS01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "M")]
        [Route("AddDieases")]
        public IHttpActionResult AddDieases(DTODIS01 objDTODIS01)
        {
            objBLDIS01Handler.ObjOperations = enmOperations.I;

            objBLDIS01Handler.PreSave(objDTODIS01);

            Response response = objBLDIS01Handler.Validation();

            if (!response.isError)
            {
                response = objBLDIS01Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates dieases
        /// </summary>
        /// <param name="objDTODIS01">Object of DIS01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "M")]
        [Route("EditDieases")]
        public IHttpActionResult EditDieases(DTODIS01 objDTODIS01)
        {
            objBLDIS01Handler.ObjOperations = enmOperations.U;

            objBLDIS01Handler.PreSave(objDTODIS01);

            Response response = objBLDIS01Handler.Validation();

            if (!response.isError)
            {
                response = objBLDIS01Handler.Save();
            }
            return Ok(response);
        }

        #endregion

    }
}
