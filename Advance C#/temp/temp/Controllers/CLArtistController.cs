using System.Web.Http;
using temp.BusinessLogic;
using temp.Models;

namespace temp.Controllers
{
    public class CLArtistController : ApiController
    {
        [HttpGet]
        [Route("api/Artist/Create")]
        public IHttpActionResult Create()
        {
            return Ok(BLArtist.Create());
        }

        [HttpPost]
        [Route("api/Artist/Insert")]
        public IHttpActionResult Insert(ART01 obj)
        {
            return Ok(BLArtist.Insert(obj));
        }
    }
}
