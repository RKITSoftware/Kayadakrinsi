using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Generics.BusinessLogic;

namespace Generics.Controllers
{
    public class CLUsersController : ApiController
    {
        [HttpGet]
        [Route("api/CLUsers/GetUsers")]
        public IHttpActionResult GetUsers() {
            return Ok(BLUsers.Users());
        }

        [Route("api/CLUsers/GetAdmins")]
        public IHttpActionResult GetAdmins()
        {
            return Ok(BLUsers.Admins());
        }

        [Route("api/CLUsers/GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(BLUsers.AllUsers());
        }
    }
}