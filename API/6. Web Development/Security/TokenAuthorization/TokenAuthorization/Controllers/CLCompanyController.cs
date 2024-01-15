using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthorization.Models;

namespace TokenAuthorization.Controllers
{
    /// <summary>
    /// Custom controller for user requests
    /// </summary>
    public class CLCompanyController : ApiController
    {
        #region Public Members

        /// <summary>
        /// List of companies
        /// </summary>
        public List<CMP01> companies = CMP01.GetCompanies();

        #endregion

        #region Public Methods

        [Authorize(Roles = ("User"))] // Authorize user with user rights
        [Route("api/Company/GetCompanyById")]
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <param name="id">Defines id of company user want to access</param>
        /// <returns>Company with given id</returns>
        public HttpResponseMessage GetCompanyById(int id)
        {
            var company = companies.FirstOrDefault(c => c.P01F01 == id);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [Authorize(Roles = ("Admin,SuperAdmin"))]
        [Route("api/Company/GetSomeCompany")]
        /// <summary>
        /// Handles get request of admin, super admin
        /// </summary>
        /// <returns>List of companies with id less than five</returns>
        public HttpResponseMessage GetSomeCompany()
        {
            var company = companies.FirstOrDefault(c => c.P01F01 <= 4);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Company/GetAllCompany")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of all companies using HttpResponseMessage</returns>
        public HttpResponseMessage GetAllCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }

        #endregion
    }
}
