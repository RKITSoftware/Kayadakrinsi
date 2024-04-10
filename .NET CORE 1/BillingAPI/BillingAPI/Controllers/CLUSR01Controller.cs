using System.Data;
using System.Web.Http.Filters;
using BillingAPI.BusinessLogic;
using BillingAPI.Filters;
using BillingAPI.Models;
using BillingAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthenticationFilter]
    [AuthorizationFilter]
    public class CLUSR01Controller : ControllerBase
    {
        public BLUSR01 objBLUser;

        public CLUSR01Controller()
        {
            objBLUser = new BLUSR01();
        }

        [HttpGet]
        [Route("GetUsers")]
        public DataTable GetUsers()
        {
            return objBLUser.Select().response;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(DTOUSR01 objDTOUSR01)
        {
            objBLUser.PreSave(objDTOUSR01);
            return Ok(objBLUser.Add());
        }
    }
}
