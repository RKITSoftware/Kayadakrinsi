using System.Net.Http;
using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    [BasicAuthenticationAttribute]
    public class CLArtistController : ApiController
    {
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Artist,Admin")]
        [Route("api/CLArtist/GetArtist")]
        public IHttpActionResult GetArtist()
        {
            return Ok(BLArtist.Select());
        }

        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Artist,Admin")]
        [Route("api/CLArtist/GetArtistFile")]
        public HttpResponseMessage GetArtistFile()
        {
            return BLArtist.Download();
        }

        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Artist,Admin")]
        [Route("api/CLArtist/AddArtist")]
        public IHttpActionResult AddArtist(ART01 objART01)
        {
            return Ok(BLArtist.Insert(objART01));
        }

        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Artist,Admin")]
        [Route("api/CLArtist/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(BLArtist.WriteData());
        }

        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Artist,Admin")]
        [Route("api/CLArtist/EditArtist")]
        public IHttpActionResult EditArtist(ART01 objART01)
        {
            return Ok(BLArtist.Update(objART01));
        }

        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLArtist/DeleteArtist")]
        public IHttpActionResult DeleteArtist(int id)
        {
            return Ok(BLArtist.Delete(id));
        }
    }
}
