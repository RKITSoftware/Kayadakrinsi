using System.Web.Http;
using temp.BusinessLogic;
using temp.Models;

namespace temp.Controllers
{
    public class CLUserController : ApiController
    {
        [HttpGet]
        [Route("api/Create")]
        public IHttpActionResult Create()
        {
            return Ok(BLUser.Create());
        }

        [HttpPost]
        [Route("api/Insert")]
        public IHttpActionResult Insert(USR01 obj)
        {
            return Ok(BLUser.Insert(obj));
        }
    }
}