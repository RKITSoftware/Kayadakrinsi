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
    /// Handles methods to perform operations related to helper
    /// </summary>
    [RoutePrefix("api/CLSTF02")]
    public class CLSTF02Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of BLSTF02Handler class
        /// </summary>
        public BLSTF02Handler objBLSTF02Handler;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public Stopwatch stopwatch;

        /// <summary>
        /// Declares object of class BLUSR01Handler
        /// </summary>
        public BLUSR01Handler objBLUSR01Handler;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLSTF02Controller()
        {
            objBLSTF02Handler = new BLSTF02Handler();
            objBLUSR01Handler = new BLUSR01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays helpers information
        /// </summary>
        /// <returns>List of helpers</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetHelpers")]
        public IHttpActionResult GetHelpers()
        {
            Response response = objBLSTF02Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of helper's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetFile")]
        public HttpResponseMessage GetFile()
        {
            return objBLSTF02Handler.Download();
        }

        /// <summary>
        /// Downloads file of current logged in helper's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "H")]
        [Route("GetMyFile")]
        public HttpResponseMessage GetMyFile()
        {
            USR01 user = objBLUSR01Handler.GetUser();

            return objBLSTF02Handler.DownloadMyFile(user);
        }

        /// <summary>
        /// Adds helper information
        /// </summary>
        /// <param name="objDTOSTF02">Object of DTOSTF02 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "M")]
        [Route("AddHelper")]
        public IHttpActionResult AddHelper(DTOSTF02 objDTOSTF02)
        {
            objBLSTF02Handler.ObjOperations = enmOperations.I;

            objBLSTF02Handler.PreSave(objDTOSTF02);

            Response response = objBLSTF02Handler.Validation();

            if (!response.isError)
            {
                response = objBLSTF02Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates helper information
        /// </summary>
        /// <param name="objDTOSTF02">Object of DTOSTF02 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "M")]
        [Route("UpdateHelper")]
        public IHttpActionResult UpdateHelper(DTOSTF02 objDTOSTF02)
        {
            objBLSTF02Handler.ObjOperations = enmOperations.U;

            objBLSTF02Handler.PreSave(objDTOSTF02);

            Response response = objBLSTF02Handler.Validation();

            if (!response.isError)
            {
                response = objBLSTF02Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes STF02 object
        /// </summary>
        /// <param name="id">Id of helper to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Authorize(Roles = "M")]
        [Route("DeleteHelper")]
        public IHttpActionResult DeleteHelper(int id)
        {
            Response response = objBLSTF02Handler.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLSTF02Handler.Delete(id);
            }

            return Ok(response);
        }

        #endregion

    }
}
