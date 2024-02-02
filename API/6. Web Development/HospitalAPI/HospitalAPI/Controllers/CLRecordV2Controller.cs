using System.Web.Http;
using HospitalAPI.BusinesLogic;
using HospitalAPI.Models;

namespace HospitalAPI.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLRecordV2Controller : ApiController
    {
        public BLRecord objBLRecord;

        public CLRecordV2Controller()
        {
            objBLRecord = new BLRecord();
        }

        #region Public Methods

        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of records with id less than four</returns>
        [HttpGet]
        [Authorize(Roles = ("User"))]
        public IHttpActionResult GetSomeRecords()
        {
            return Ok(objBLRecord.GetSomeRecords());
        }

        ///// <summary>
        ///// Handles get request of admin, super admin
        ///// </summary>
        ///// <returns>List of records with id less than seven</returns>
        [HttpGet]
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        public IHttpActionResult GetMoreRecords()
        {
            return Ok(objBLRecord.GetMoreRecords());
        }

        ///// <summary>
        ///// Handles get request of super admin
        ///// </summary>
        ///// <returns>List of all records using HttpResponseMessage</returns>
        [HttpGet]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult GetAllRecords()
        {
            return Ok(BLRecord.lstRCD01);
        }

        /// <summary>
        /// Hadles post request of super admin
        /// </summary>
        /// <param name="id">id of new record</param>
        /// <param name="newrecord">record to be add</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        public IHttpActionResult AddRecord(RCD01 objRCD01)
        {
            return Ok(objBLRecord.AddRecord(objRCD01));
        }

        [HttpPut]
        [Authorize(Roles = ("SuperAdmin"))]
        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="id">id of record to be edit</param>
        /// <param name="editedrecord">record with changes</param>
        /// <returns>List of records</returns>
        public IHttpActionResult EditRecord(RCD01 objRCD01)
        {
            return Ok(objBLRecord.EditRecord(objRCD01));
        }

        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of record to be delete</param>
        /// <returns>List of records</returns>
        [HttpDelete]
        [Authorize(Roles = ("SuperAdmin"))]
        public IHttpActionResult DeleteRecord(int id)
        {
            return Ok(objBLRecord.DeleteRecord(id));
        }

        #endregion
    }
}
