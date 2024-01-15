using Authentication_Authorization.BasicAuth;
using Authentication_Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication_Authorization.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Company")]
    [BasicAuthenticationAttribute]

    /// <summary>
    /// Custom cntroller for handling requests
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

        [Route("GetFewCompanies")]
        [BasicAuthorizationAttribute(Roles = "User")] // Custom filter attribute for authorization with role of user
        /// <summary>
        /// Handles get request of normal user
        /// </summary>
        /// <returns>List of compaines which can be access by normal user</returns>
        public HttpResponseMessage GetFewCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.P01F01 <= 2));
        }

        [Route("GetMoreCompanies")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        /// <summary>
        /// Handles get request of admin
        /// </summary>
        /// <returns>List of compaines which can be access by admin</returns>
        public HttpResponseMessage GetMoreCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.P01F01 <= 3));
        }

        [Route("GetAllCompanies")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        /// <summary>
        /// Handles get request of super admin
        /// </summary>
        /// <returns>List of compaines which can be access by super admin</returns>
        public HttpResponseMessage GetAllCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, CMP01.GetCompanies());
        }

        //[Route("PostCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage PostCompany(int id,Company newCompany){
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company == null)
        //        {
        //            companies.Add(newCompany);
        //            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.Id <= 3));
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.BadRequest, "Company with given id is already exist!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        //[Route("PutCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage PutCompany(int id,Company editedCompany)
        //{
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company != null)
        //        {
        //            companies[id]=editedCompany;
        //            return Request.CreateResponse(HttpStatusCode.OK, companies);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Company with given id not found!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        //[Route("DeleteCompany")]
        //[BasicAuthorizationAttribute(Roles = "Admin")]
        //public HttpResponseMessage DeleteCompany(int id)
        //{
        //    var company = companies.Find(x => x.Id == id);
        //    try
        //    {
        //        if (company != null)
        //        {
        //            companies.RemoveAt(id);
        //            return Request.CreateResponse(HttpStatusCode.OK, companies);
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotFound, "Company with given id not found!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Ambiguous, ex.Message);
        //    }
        //}

        #endregion
    }
}
