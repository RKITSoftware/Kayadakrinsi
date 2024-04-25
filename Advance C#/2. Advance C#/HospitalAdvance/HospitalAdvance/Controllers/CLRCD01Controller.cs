using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Handles methods to preform operations related to record
    /// </summary>
    [RoutePrefix("api/CLRCD01")]
    public class CLRCD01Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of BLRCD01Handler
        /// </summary>
        public BLRCD01Handler objBLRCD01;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public Stopwatch stopwatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLRCD01Controller()
        {
            objBLRCD01 = new BLRCD01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays records
        /// </summary>
        /// <returns>Appropriate Response object</returns>
        [HttpGet]
        [Authorize(Roles = "M,D,H,P")]
        [Route("GetRecords")]
        public IHttpActionResult GetRecords()
        {
            Response response = objBLRCD01.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of record's response
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "M,D,H,P")]
        [Route("GetFile")]
        public HttpResponseMessage GetFile()
        {
            return objBLRCD01.Download();
        }

        /// <summary>
        /// Adds record information
        /// </summary>
        /// <param name="objDTORCD01">Object of DTORCD01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "M")]
        [Route("AddRecord")]
        public IHttpActionResult AddRecord(DTORCD01 objDTORCD01)
        {
            Response response = objBLRCD01.PreValidation(objDTORCD01);

            if (!response.isError)
            {
                objBLRCD01.ObjOperations = enmOperations.I;

                objBLRCD01.PreSave(objDTORCD01);

                response = objBLRCD01.Validation();

                if (!response.isError)
                {
                    response = objBLRCD01.Save();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates record information
        /// </summary>
        /// <param name="objDTORCD01">Object of DTORCD01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "M")]
        [Route("EditRecord")]
        public IHttpActionResult EditRecord(DTORCD01 objDTORCD01)
        {
            Response response = objBLRCD01.PreValidation(objDTORCD01);

            if (!response.isError)
            {
                objBLRCD01.ObjOperations = enmOperations.U;

                objBLRCD01.PreSave(objDTORCD01);

                response = objBLRCD01.Validation();

                if (!response.isError)
                {
                    response = objBLRCD01.Save();
                }
            }

            return Ok(response);
        }

        #endregion

    }
}
