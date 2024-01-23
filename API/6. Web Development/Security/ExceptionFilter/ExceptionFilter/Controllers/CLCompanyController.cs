using System;
using System.Web.Http;
using ExceptionFilter.BusinessLogic;

namespace ExceptionFilter.Controllers
{
    public class CLCompanyController : ApiController
    {
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        [HttpGet]
        [Route("api/CLCompany/GetFewCompanies")]
        public IHttpActionResult GetFewCompanies()
        {
            return Ok(BLCompany.GetFewCompanies());
        }

        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        [HttpGet]
        [Route("api/CLCompany/GetMoreCompanies")]
        public IHttpActionResult GetMoreCompanies()
        {
            return Ok(BLCompany.GetMoreCompanies());
        }

        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        [HttpGet]
        [Route("api/CLCompany/GetAllCompanies")]
        public IHttpActionResult GetAllCompanies()
        {
            return Ok(BLCompany.lstCompanies);
        }

        /// <summary>
        /// Not implemented method
        /// </summary>
        /// <param name="id">id of company</param>
        /// <returns>Exception response</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("api/CLCompany/GetCompany/{id}")]
        [NotImplExceptionFilter]
        public IHttpActionResult GetCompany(int id)
        {
            throw new NotImplementedException("This method is not implemented");
        }
    }
}
