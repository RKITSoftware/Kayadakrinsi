using System.Web.Http;
using ORM.BusinessLogic;
using ORM.Models;

namespace ORM.Controllers
{
    /// <summary>
    /// Company controller
    /// </summary>
    public class CLCompanyController : ApiController
    {
        /// <summary>
        /// Declares object of class BLCompany
        /// </summary>
        private readonly BLCompany objBLComapny;

        /// <summary>
        /// Intializes object of class BLCompany
        /// </summary>
        public CLCompanyController()
        {
            objBLComapny = new BLCompany();
        }

        /// <summary>
        /// Gets all data of company
        /// </summary>
        /// <returns>List of companies</returns>
        [HttpGet]
        [Route("api/CLCompany/GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(objBLComapny.Select());
        }

        /// <summary>
        /// Adds company into the list
        /// </summary>
        /// <param name="objCMP01">object of class CMP01 which will be added</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("api/CLCompany/AddCompanies")]
        public IHttpActionResult AddCompanies(CMP01 objCMP01)
        {
            return Ok(objBLComapny.Insert(objCMP01));
        }

        /// <summary>
        /// Edits company into the list
        /// </summary>
        /// <param name="objCMP01">object of class CMP01 which will be edit</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("api/CLCompany/EditCompanies")]
        public IHttpActionResult EditCompanies(CMP01 objCMP01)
        {
            return Ok(objBLComapny.Update(objCMP01));
        }

        /// <summary>
        /// Deletes company from list
        /// </summary>
        /// <param name="id">id of company which will be deleted</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("api/CLCompany/DeleteCompany")]
        public IHttpActionResult DeleteCompany(int id)
        {
            return Ok(objBLComapny.Delete(id));
        }
    }
}
