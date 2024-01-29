using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    [BasicAuthenticationAttribute]
    public class CLUserController : ApiController
    {
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/GetUsers")]
        public IHttpActionResult GetUsers()
        {
            return Ok(BLUser.Select());
        }

        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/AddUser")]
        public IHttpActionResult AddUser(USR01 objUSR01)
        {
            return Ok(BLUser.Insert(objUSR01));
        }

        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/EditUser")]
        public IHttpActionResult EditUsers(USR01 objUSR01)
        {
            return Ok(BLUser.Update(objUSR01));
        }

        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/DeleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            return Ok(BLUser.Delete(id));
        }
    }
}
