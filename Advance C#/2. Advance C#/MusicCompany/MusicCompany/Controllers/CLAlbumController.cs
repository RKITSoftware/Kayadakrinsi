using System.Net.Http;
using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    [BasicAuthenticationAttribute]
    public class CLAlbumController : ApiController
    {
            [HttpGet]
            [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
            [Route("api/CLArtist/GetAlbum")]
            public IHttpActionResult GetAlbum()
            {
                return Ok(BLArtist.Select());
            }

            [HttpGet]
            [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
            [Route("api/CLArtist/GetAlbumFile")]
            public HttpResponseMessage GetAlbumFile()
            {
                return BLAlbum.Download();
            }

            [HttpPost]
            [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
            [Route("api/CLAlbum/AddAlbum")]
            public IHttpActionResult AddAlbum(ALB01 objALB01)
            {
                return Ok(BLAlbum.Insert(objALB01));
            }

            [HttpPost]
            [BasicAuthorizationAttribute(Roles = "Album,Producer,Admin")]
            [Route("api/CLAlbum/WriteFile")]
            public IHttpActionResult WriteFile()
            {
                return Ok(BLAlbum.WriteData());
            }

            [HttpPut]
            [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
            [Route("api/CLAlbum/EditAlbum")]
            public IHttpActionResult EditAlbum(ALB01 objALB01)
            {
                return Ok(BLAlbum.Update(objALB01));
            }

            [HttpDelete]
            [BasicAuthorizationAttribute(Roles = "Admin")]
            [Route("api/CLAlbum/DeleteAlbum")]
            public IHttpActionResult DeleteAlbum(int id)
            {
                return Ok(BLAlbum.Delete(id));
            }
        }
}