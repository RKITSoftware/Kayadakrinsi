using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    /// <summary>
    /// Handles methods to predorm operations related to user
    /// </summary>
    [BasicAuthenticationAttribute]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Handles get reuest for getting user data
        /// </summary>
        /// <returns>All data from table USR01</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/GetUsers")]
        public IHttpActionResult GetUsers()
        {
            return Ok(BLUser.Select());
        }

        /// <summary>
        /// Get details of album with producer and artist details
        /// </summary>
        /// <returns>List of data from joins</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/GetAllDetails")]
        public IHttpActionResult GetAllDetails()
        {
            return Ok(BLUser.SelectAllDetails());
        }

        /// <summary>
        /// Adds user
        /// </summary>
        /// <param name="objUSR01">object of USR01 class to be added</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Admin")]

        [Route("api/CLUser/AddUser")]
        public IHttpActionResult AddUser(USR01 objUSR01)
        {
            return Ok(BLUser.Insert(objUSR01));
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="objUSR01">object of USR01 class to be edit</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/EditUser")]
        public IHttpActionResult EditUsers(USR01 objUSR01)
        {
            return Ok(BLUser.Update(objUSR01));
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">id of user to be delete</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLUser/DeleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            return Ok(BLUser.Delete(id));
        }
    }
}
