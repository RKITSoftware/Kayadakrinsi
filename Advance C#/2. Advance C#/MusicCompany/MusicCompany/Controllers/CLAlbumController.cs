using System.Net.Http;
using System.Web.Http;
using MusicCompany.Auth;
using MusicCompany.BusinessLogic;
using MusicCompany.Filter;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    /// <summary>
    /// Handles methods to predorm operations related to album
    /// </summary>
    // [BasicAuthenticationAttribute]
    [CustomExceptionFilter]
	public class CLAlbumController : ApiController
    {
        /// <summary>
        /// Declares object of class BLAlbum
        /// </summary>
        public BLAlbum objBLAlbum;

        /// <summary>
        /// Initializes object of class BLAlbum
        /// </summary>
        public CLAlbumController()
        {
            objBLAlbum = new BLAlbum();
        }

        /// <summary>
        /// Handles get reuest for getting album data
        /// </summary>
        /// <returns>All data from table ALB01</returns>
        [HttpGet]
        [BearerAuthentication]
        [Authorize(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/GetAlbum")]
        public IHttpActionResult GetAlbum()
        {
            return Ok(objBLAlbum.Select());
        }

        /// <summary>
        /// Downloads file of ablum data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/GetAlbumFile")]
        public HttpResponseMessage GetAlbumFile()
        {
            return objBLAlbum.Download();
        }

        /// <summary>
        /// Adds album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class to be added</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BearerAuthentication]
		[Authorize(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/AddAlbum")]
        public IHttpActionResult AddAlbum(ALB01 objALB01)
        {
            return Ok(objBLAlbum.Insert(objALB01));
        }

        /// <summary>
        /// Write data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BearerAuthentication]
		[Authorize(Roles = "Album,Producer,Admin")]
        [Route("api/CLAlbum/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(objBLAlbum.WriteData());
        }

        /// <summary>
        /// Edit album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class to be edit</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [BearerAuthentication]
		[Authorize(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/EditAlbum")]
        public IHttpActionResult EditAlbum(ALB01 objALB01)
        {
            return Ok(objBLAlbum.Update(objALB01));
        }

        /// <summary>
        /// Deletes album
        /// </summary>
        /// <param name="id">id of album to be deleted</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
        [BearerAuthentication]
		[Authorize(Roles = "Admin")]
        [Route("api/CLAlbum/DeleteAlbum")]
        public IHttpActionResult DeleteAlbum(int id)
        {
            return Ok(objBLAlbum.Delete(id));
        }
    }
}