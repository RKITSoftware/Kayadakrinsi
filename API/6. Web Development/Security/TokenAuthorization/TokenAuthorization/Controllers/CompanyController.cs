using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthorization.Models;

namespace TokenAuthorization.Controllers
{
    public class CompanyController : ApiController
    {
        public List<Company> companies = Company.GetCompanies();
        [Authorize(Roles = ("User"))]
        [Route("api/Company/GetCompanyById")]
        public HttpResponseMessage GetCompanyById(int id)
        {
            var company = companies.FirstOrDefault(c => c.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [Authorize(Roles = ("Admin,SuperAdmin"))]
        [Route("api/Company/GetSomeCompany")]
        public HttpResponseMessage GetSomeCompany()
        {
            var company = companies.FirstOrDefault(c => c.Id <= 4);
            return Request.CreateResponse(HttpStatusCode.OK, company);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Company/GetAllCompany")]
        public HttpResponseMessage GetAllCompany()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies);
        }
    }
}
