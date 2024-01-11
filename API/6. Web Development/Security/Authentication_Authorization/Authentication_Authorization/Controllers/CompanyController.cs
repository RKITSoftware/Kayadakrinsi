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
    [BasicAuthenticationAttribute]
    public class CompanyController : ApiController
    {
        public List<Company> GetCompanies()
        {
            return Company.GetCompnies();
        }
    }
}
