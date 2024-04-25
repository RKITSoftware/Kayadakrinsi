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
    /// Handles methods to perform operations related to doctor
    /// </summary>
    [RoutePrefix("api/CLSTF01")]
    public class CLSTF01Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of BLSTF01Handler class
        /// </summary>
        public BLSTF01Handler objBLSTF01Handler;

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
        /// Initializes object of BLDoctor class
        /// </summary>
        public CLSTF01Controller()
        {
            objBLSTF01Handler = new BLSTF01Handler();
            objBLUSR01Handler = new BLUSR01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays doctors information
        /// </summary>
        /// <returns>List of doctors</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetDoctors")]
        public IHttpActionResult GetDoctors()
        {
            Response response = objBLSTF01Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of doctor's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetFile")]
        public HttpResponseMessage GetFile()
        {
            return objBLSTF01Handler.Download();
        }

        /// <summary>
        /// Downloads file of current logged in doctor's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "D")]
        [Route("GetMyFile")]
        public HttpResponseMessage GetMyFile()
        {
            USR01 user = objBLUSR01Handler.GetUser();
            return objBLSTF01Handler.DownloadMyFile(user);
        }

        /// <summary>
        /// Adds doctor information
        /// </summary>
        /// <param name="objDTOSTF01">Object of DTOSTF01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "M")]
        [Route("AddDoctor")]
        public  IHttpActionResult AddDoctor(DTOSTF01 objDTOSTF01)
        {
            objBLSTF01Handler.ObjOperations = enmOperations.I;

            objBLSTF01Handler.PreSave(objDTOSTF01);

            Response response = objBLSTF01Handler.Validation();

            if (!response.isError)
            {
                response = objBLSTF01Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates doctor information
        /// </summary>
        /// <param name="objDTOSTF01">Object of DTOSTF01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "M")]
        [Route("UpdateDoctor")]
        public  IHttpActionResult UpdateDoctor(DTOSTF01 objDTOSTF01)
        {
            objBLSTF01Handler.ObjOperations = enmOperations.U;

            objBLSTF01Handler.PreSave(objDTOSTF01);

            Response response = objBLSTF01Handler.Validation();

            if (!response.isError)
            {
                response =  objBLSTF01Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes STF01 object
        /// </summary>
        /// <param name="id">Id of doctor to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Authorize(Roles = "M")]
        [Route("DeleteDoctor")]
        public IHttpActionResult DeleteDoctor(int id)
        {

            Response response = objBLSTF01Handler.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLSTF01Handler.Delete(id);
            }

            return Ok(response);
        }


        #endregion

    }
}
