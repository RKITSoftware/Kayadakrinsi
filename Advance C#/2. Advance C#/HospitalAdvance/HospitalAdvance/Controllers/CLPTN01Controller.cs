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
    /// Handles methods to perform operations related to patients
    /// </summary>
    [RoutePrefix("api/CLPTN01")]
    public class CLPTN01Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// Declares object of BLPTN01Handler class
        /// </summary>
        public BLPTN01Handler objBLPTN01Handler;

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
        public CLPTN01Controller()
        {
            objBLPTN01Handler = new BLPTN01Handler();
            objBLUSR01Handler = new BLUSR01Handler();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Displays patients information
        /// </summary>
        /// <returns>List of patients</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetPatients")]
        public IHttpActionResult GetPatients()
        {
            Response response = objBLPTN01Handler.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(response);
        }

        /// <summary>
        /// Downloads file of patient's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "M")]
        [Route("GetFile")]
        public HttpResponseMessage GetFile()
        {
            return objBLPTN01Handler.Download();
        }

        /// <summary>
        /// Downloads file of current logged in patient's data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [Authorize(Roles = "P")]
        [Route("GetMyFile")]
        public HttpResponseMessage GetMyFile()
        {
            USR01 user = objBLUSR01Handler.GetUser();
            return objBLPTN01Handler.DownloadMyFile(user);
        }

        /// <summary>
        /// Displays particular patient's charges detials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "P")]
        [Route("GetMyRecipt")]
        public IHttpActionResult GetMyRecipt()
        {
            var user = objBLUSR01Handler.GetUser();
            return Ok(objBLPTN01Handler.GetMyRecipt(user));
        }

        /// <summary>
        /// Adds patient information
        /// </summary>
        /// <param name="objDTOPTN01">Object of DTOPTN01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Authorize(Roles = "M")]
        [Route("AddPatient")]
        public IHttpActionResult AddPatient(DTOPTN01 objDTOPTN01)
        {
            objBLPTN01Handler.ObjOperations = enmOperations.I;

            objBLPTN01Handler.PreSave(objDTOPTN01);

            Response response = objBLPTN01Handler.Validation();

            if (!response.isError)
            {
                response = objBLPTN01Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates patient information
        /// </summary>
        /// <param name="objDTOPTN01">Object of DTOPTN01 class</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Authorize(Roles = "M")]
        [Route("UpdatePatient")]
        public IHttpActionResult UpdatePatient(DTOPTN01 objDTOPTN01)
        {
            objBLPTN01Handler.ObjOperations = enmOperations.U;

            objBLPTN01Handler.PreSave(objDTOPTN01);

            Response response = objBLPTN01Handler.Validation();

            if (!response.isError)
            {
                response = objBLPTN01Handler.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes PTN01 object
        /// </summary>
        /// <param name="id">Id of patient to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Authorize(Roles = "M")]
        [Route("DeletePatient")]
        public IHttpActionResult DeletePatient(int id)
        {
            Response response = objBLPTN01Handler.ValidationDelete(id);

            if (!response.isError)
            {
                response = objBLPTN01Handler.Delete(id);
            }

            return Ok(response);
        }

        #endregion

    }
}
