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
    public class CompanyController : ApiController
    {
        List<Company> companies = Company.GetCompanies();
        // Get few compnies
        [Route("GetFewCompanies")]
        [BasicAuthorizationAttribute(Roles = "User")]
        public HttpResponseMessage GetFewCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.Id <= 2));
        }

        // Get more compnies
        [Route("GetMoreCompanies")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        public HttpResponseMessage GetMoreCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, companies.Where(c => c.Id <= 3));
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

        // Get all compnies
        [Route("GetAllCompanies")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public HttpResponseMessage GetAllCompanies()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Company.GetCompanies());
        }

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
    }
}
