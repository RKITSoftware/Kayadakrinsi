using System;
using System.Collections.Generic;
using System.Web.Http;
using HospitalAPI.Models;

namespace HospitalAPI.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLRecordV2Controller : ApiController
    {

        #region Public Members

        /// <summary>
        /// List of records
        /// </summary>
        public static List<RCD01> records = RCD01.GetRecords();

        #endregion

        #region Public Methods

        [HttpGet]
        [Authorize(Roles = ("User"))]
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of records with id less than four</returns>
        public IHttpActionResult GetSomeRecords()
        {
            var company = records.FindAll(c => c.D01F01 < 4);
            return Ok(company);
        }

        [Authorize(Roles = ("Admin,SuperAdmin"))]
        ///// <summary>
        ///// Handles get request of admin, super admin
        ///// </summary>
        ///// <returns>List of records with id less than seven</returns>
        public IHttpActionResult GetMoreRecords()
        {
            var record = records.FindAll(r => r.D01F01 < 7 );
            return Ok(record);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        ///// <summary>
        ///// Handles get request of super admin
        ///// </summary>
        ///// <returns>List of all records using HttpResponseMessage</returns>
        public IHttpActionResult GetAllRecords()
        {
            return Ok(records);
        }

        [HttpPost]
        [Authorize(Roles = ("Admin"))]
        /// <summary>
        /// Handles post request of admin
        /// </summary>
        /// <param name="id">id of new record</param>
        /// <param name="newrecord">record to be add</param>
        /// <returns>List of records</returns>
        public IHttpActionResult AddRecordAdmin(int id, RCD01 newrecord)
        {
            var record = records.Find(r => r.D01F01 == id);
            try
            {
                if (record == null)
                {
                    records.Add(newrecord);
                    return Ok(records.FindAll(r => r.D01F01 < 7));
                }
                else
                {
                    return Ok("Id is already exist ; try again with different id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = ("SuperAdmin"))]
        /// <summary>
        /// Hadles post request of super admin
        /// </summary>
        /// <param name="id">id of new record</param>
        /// <param name="newrecord">record to be add</param>
        /// <returns></returns>
        public IHttpActionResult AddRecord(int id, RCD01 newrecord)
        {
            var record = records.Find(r => r.D01F01 == id);
            try
            {
                if (record == null)
                {
                    records.Add(newrecord);
                    return Ok(records);
                }
                else
                {
                    return Ok("Id is already exist ; try again with different id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = ("SuperAdmin"))]
        /// <summary>
        /// Handles put request
        /// </summary>
        /// <param name="id">id of record to be edit</param>
        /// <param name="editedrecord">record with changes</param>
        /// <returns>List of records</returns>
        public IHttpActionResult EditRecord(int id,RCD01 editedrecord)
        {
            var record = records.Find(r => r.D01F01 == id);
            try
            {
                if (record != null)
                {
                    records[id] = editedrecord;
                    return Ok(records);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = ("SuperAdmin"))]
        /// <summary>
        /// Handles delete request
        /// </summary>
        /// <param name="id">id of record to be delete</param>
        /// <returns>List of records</returns>
        public IHttpActionResult DeleteRecord(int id)
        {
            var record = records.Find(r => r.D01F01 == id);
            try
            {
                if (record != null)
                {
                    records.RemoveAt(id);
                    return Ok(records);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
