using System.Collections.Generic;
using System.Web.Http;
using ORM.BusinessLogic;
using ORM.Models;
using ServiceStack.Text;

namespace ORM.Controllers
{
    /// <summary>
    /// Company controller
    /// </summary>
    [RoutePrefix("api/CLCMP01Controller")]
    public class CLCMP01Controller : ApiController
    {
        /// <summary>
        /// Declares object of class BLCompany
        /// </summary>
        private readonly BLCMP01 objBLCMP01;

        /// <summary>
        /// Intializes object of class BLCompany
        /// </summary>
        public CLCMP01Controller()
        {
            objBLCMP01 = new BLCMP01();
        }

        /// <summary>
        /// Gets all data of companies.
        /// </summary>
        /// <returns>List of companies.</returns>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(objBLCMP01.Select());
        }

        /// <summary>
        /// Gets data of companies by IDs.
        /// </summary>
        /// <param name="lstIDs">List of company IDs.</param>
        /// <returns>List of companies.</returns>
        [HttpGet]
        [Route("GetByIDs")]
        public IHttpActionResult GetByIDs([FromUri] List<int> lstIDs)
        {
            return Ok(objBLCMP01.SelectByIDs(lstIDs));
        }

        /// <summary>
        /// Gets data of company by ID.
        /// </summary>
        /// <param name="id">The ID of the company.</param>
        /// <returns>The company with the specified ID.</returns>
        [HttpGet]
        [Route("GetByID")]
        public IHttpActionResult GetByID(int id)
        {
            return Ok(objBLCMP01.SingleById(id));
        }

        /// <summary>
        /// Adds a company into the list.
        /// </summary>
        /// <returns>Appropriate message.</returns>
        [HttpPost]
        [Route("InsertOnly")]
        public IHttpActionResult InsertOnly()
        {
            return Ok(objBLCMP01.InsertOnly());
        }

        /// <summary>
        /// Inserts a company into the database.
        /// </summary>
        /// <param name="objDTOCMP01">The DTO object representing the company to be inserted.</param>
        /// <returns>Appropriate message.</returns>
        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult Insert(DTOCMP01 objDTOCMP01)
        {
            objBLCMP01.PreSave(objDTOCMP01);
            return Ok(objBLCMP01.Insert());
        }

        /// <summary>
        /// Inserts multiple companies into the database.
        /// </summary>
        /// <param name="LstDTOCMP01">List of DTO objects representing the companies to be inserted.</param>
        /// <returns>Appropriate message.</returns>
        [HttpPost]
        [Route("InsertAll")]
        public IHttpActionResult InsertAll(List<DTOCMP01> LstDTOCMP01)
        {
            objBLCMP01.PreSave(LstDTOCMP01);
            return Ok(objBLCMP01.InsertAll());
        }


        /// <summary>
        /// Edits a company in the list.
        /// </summary>
        /// <returns>Appropriate message.</returns>
        [HttpPut]
        [Route("UpdateOnly")]
        public IHttpActionResult UpdateOnly()
        {
            return Ok(objBLCMP01.UpdateOnly());
        }

        /// <summary>
        /// Edits a company in the list.
        /// </summary>
        /// <param name="id">The ID of the company to be edited.</param>
        /// <param name="objCMP01">Object of class CMP01 representing the company to be edited.</param>
        /// <returns>Appropriate message.</returns>
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(int id, DTOCMP01 objCMP01)
        {
            objBLCMP01.PreSave(objCMP01);
            return Ok(objBLCMP01.Update(id));
        }

        /// <summary>
        /// Edits multiple companies in the list.
        /// </summary>
        /// <param name="lstIDs">List of IDs of the companies to be edited.</param>
        /// <param name="LstCMP01">List of DTO objects representing the companies to be edited.</param>
        /// <returns>Appropriate message.</returns>
        [HttpPut]
        [Route("UpdateAll")]
        public IHttpActionResult UpdateAll()
        {
            return Ok(objBLCMP01.UpdateAll());
        }

        /// <summary>
        /// Updates specific fields of a company in the list.
        /// </summary>
        /// <returns>Appropriate message.</returns>
        [HttpPut]
        [Route("UpdateOnlyFields")]
        public IHttpActionResult UpdateOnlyFields()
        {
            return Ok(objBLCMP01.UpdateOnlyFields());
        }

        /// <summary>
        /// Deletes a company from the list.
        /// </summary>
        /// <param name="id">The ID of the company to be deleted.</param>
        /// <returns>Appropriate message.</returns>
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            return Ok(objBLCMP01.Delete(id));
        }

        /// <summary>
        /// Deletes a company from the list.
        /// </summary>
        /// <param name="id">The ID of the company to be deleted.</param>
        /// <returns>Appropriate message.</returns>
        [HttpDelete]
        [Route("DeleteById")]
        public IHttpActionResult DeleteById(int id)
        {
            return Ok(objBLCMP01.DeleteById(id));
        }

        /// <summary>
        /// Deletes all companies from the list.
        /// </summary>
        /// <returns>Appropriate message.</returns>
        [HttpDelete]
        [Route("DeleteAll")]
        public IHttpActionResult DeleteAll()
        {
            return Ok(objBLCMP01.DeleteAll());
        }

        /// <summary>
        /// Gets schema tables.
        /// </summary>
        /// <returns>Schema tables.</returns>
        [HttpGet]
        [Route("GetSchemaTables")]
        public IHttpActionResult GetSchemaTables()
        {
            var result = objBLCMP01.GetSchemaTables();
            result.PrintDump();
            return Ok(result);
        }

        /// <summary>
        /// Executes a scalar query.
        /// </summary>
        /// <returns>Scalar query result.</returns>
        [HttpGet]
        [Route("Scalar")]
        public IHttpActionResult Scalar()
        {
            var result = objBLCMP01.Scalar();
            return Ok(result);
        }

        /// <summary>
        /// Executes a group by query.
        /// </summary>
        /// <returns>Grouped result.</returns>
        [HttpGet]
        [Route("GroupBy")]
        public IHttpActionResult GroupBy()
        {
            var result = objBLCMP01.GroupBy();
            return Ok(result);
        }

    }
}
