using System.Web.Http;
using ORM.BusinessLogic;
using ORM.Models;

namespace ORM.Controllers
{
    public class CLCompanyController : ApiController
    {
        private readonly BLCompany objBLComapny;

        public CLCompanyController()
        {
            objBLComapny = new BLCompany();
        }
        [HttpGet]
        [Route("api/CLCompany/GetAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(objBLComapny.Select());
        }

        [HttpPost]
        [Route("api/CLCompany/AddCompanies")]
        public IHttpActionResult AddCompanies(CMP01 objCMP01)
        {
            return Ok(objBLComapny.Insert(objCMP01));
        }

        [HttpPut]
        [Route("api/CLCompany/EditCompanies")]
        public IHttpActionResult EditCompanies(CMP01 objCMP01)
        {
            return Ok(objBLComapny.Update(objCMP01));
        }

        [HttpDelete]
        [Route("api/CLCompany/DeleteCompany")]
        public IHttpActionResult DeleteCompany(int id)
        {
            return Ok(objBLComapny.Delete(id));
        }
    }
}
