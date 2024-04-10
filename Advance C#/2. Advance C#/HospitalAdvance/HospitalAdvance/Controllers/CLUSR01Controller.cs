using System.Diagnostics;
using System.Web;
using System.Web.Http;
using HospitalAdvance.Auth;
using HospitalAdvance.BusinessLogic;
using HospitalAdvance.Models;

namespace HospitalAdvance.Controllers
{
    /// <summary>
    /// Handles methods to predorm operations related to user
    /// </summary>
    public class CLUSR01Controller : ApiController
    {
        #region Public Members

        /// <summary>
        /// Declares object of BLUser class
        /// </summary>
        public BLUSR01 objBLUser;

        /// <summary>
        /// Declares object of Stopwatch class
        /// </summary>
        public static Stopwatch stopwatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes objects
        /// </summary>
        public CLUSR01Controller()
        {
            objBLUser = new BLUSR01();
            stopwatch = Stopwatch.StartNew();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles get reuest for getting user data
        /// </summary>
        /// <returns>All data from table USR01</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = "Manager")]
        [Route("api/CLUser/GetUsers")]
        public IHttpActionResult GetUsers()
        {
            var data = objBLUser.Select();

            stopwatch.Stop();
            long responseTime = stopwatch.ElapsedTicks;

            HttpContext.Current.Response.AddHeader("Response-time", responseTime.ToString());

            return Ok(data);
        }

        /// <summary>
        /// Handles get reuest for getting user data
        /// </summary>
        /// <returns>All data from table USR01</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = "Manager")]
        [Route("api/CLUser/GetCache")]
        public IHttpActionResult GetCache()
        {
            return Ok(objBLUser.GetCache());
        }

        /// <summary>
        /// Adds users 
        /// </summary>
        /// <param name="model">object of USR01,object of their respect role's model</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [BearerAuthentication]
        [Authorize(Roles = "Manager")]
        [Route("api/CLUser/AddUser")]
        public IHttpActionResult AddUser([FromBody] USR02 model)
        {
            if (objBLUser.preValidation(model))
            {
                model.ObjUSR01 = objBLUser.preSave(model.ObjUSR01);
                if (objBLUser.validation(model.ObjUSR01))
                {
                    var result = objBLUser.InsertData(model);
                    if(result == "Invalid data")
                    {
                        return BadRequest("Invalid data");
                    }
                    return Ok("Success!");
                }
            }
            return BadRequest("Invalid data");
        }

        /// <summary>
        /// Updates user
        /// </summary>
        ///  <param name="model">object of USR01,object of their respect role's model</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [BearerAuthentication]
        [Authorize(Roles = "Manager")]
        [Route("api/CLUser/EditUser")]
        public IHttpActionResult EditUsers([FromBody] USR02 model)
        {
            if (objBLUser.preValidation(model))
            {
                model.ObjUSR01 = objBLUser.preSave(model.ObjUSR01);
                if (objBLUser.validation(model.ObjUSR01))
                {
                    objBLUser.Update(model.ObjUSR01);
                    objBLUser.UpdateData(model);
                    return Ok("Success!");
                }
            }
            return BadRequest("Invalid data");
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">id of user to be delete</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
        [BearerAuthentication]
        [Authorize(Roles = "Manager")]
        [Route("api/CLUser/DeleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (objBLUser.validationDelete(id))
            {
                objBLUser.DeleteData(id);
                return Ok("Success!");
            }
            return BadRequest("Invalid data");
        }

        #endregion

    }

}
