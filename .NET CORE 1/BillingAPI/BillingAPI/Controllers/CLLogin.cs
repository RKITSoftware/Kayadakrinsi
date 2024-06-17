using BillingAPI.BusinessLogic;
using BillingAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLLogin : ControllerBase
    {
        public BLLogin objBLLogin = new BLLogin();

        public BLTokenManager objBLTokenManager = new BLTokenManager();

        /// <summary>
        /// Generates token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GenerateToken")]
        public IActionResult GenerateToken(string username,string password)
        {
            var userDetails = objBLLogin.ValidateUser(username,password);

            if (userDetails != null)
            {
                return Ok(objBLTokenManager.GenerateToken(userDetails));
            }
            return BadRequest("Enter valid user details");
        }
    }
}
