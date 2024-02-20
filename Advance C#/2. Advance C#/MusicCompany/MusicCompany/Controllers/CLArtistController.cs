using System.Net.Http;
using System.Web.Http;
using MusicCompany.Auth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
	/// <summary>
	/// Handles methods to predorm operations related to artist
	/// </summary>
	// [BasicAuthenticationAttribute]
	public class CLArtistController : ApiController
    {
        /// <summary>
        /// Declares object of BLArtist class
        /// </summary>
        public BLArtist objBLArtist;

        /// <summary>
        /// Initializes object of BLArtist class
        /// </summary>
        public CLArtistController()
        {
            objBLArtist = new BLArtist();
        }

        /// <summary>
        /// Handles get reuest for getting artist data
        /// </summary>
        /// <returns>All data from table ART01</returns>
        [HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Admin")]
        [Route("api/CLArtist/GetArtist")]
        public IHttpActionResult GetArtist()
        {
            return Ok(objBLArtist.Select());
        }

        /// <summary>
        /// Downloads file of artist data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Admin")]
        [Route("api/CLArtist/GetArtistFile")]
        public HttpResponseMessage GetArtistFile()
        {
            return objBLArtist.Download();
        }

        /// <summary>
        /// Adds artist
        /// </summary>
        /// <param name="objART01">object of ART01 class to be added</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Admin")]
        [Route("api/CLArtist/AddArtist")]
        public IHttpActionResult AddArtist(ART01 objART01)
        {
            return Ok(objBLArtist.Insert(objART01));
        }

        /// <summary>
        /// Write data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Admin")]
        [Route("api/CLArtist/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(objBLArtist.WriteData());
        }

        /// <summary>
        /// Edits artist
        /// </summary>
        /// <param name="objART01">object of ART01 class to be edit</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
		[BearerAuthentication]
		[Authorize(Roles = "Artist,Admin")]
        [Route("api/CLArtist/EditArtist")]
        public IHttpActionResult EditArtist(ART01 objART01)
        {
            return Ok(objBLArtist.Update(objART01));
        }

        /// <summary>
        /// Deletes artist
        /// </summary>
        /// <param name="id">id of artist to be deleted</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
		[BearerAuthentication]
		[Authorize(Roles = "Admin")]
        [Route("api/CLArtist/DeleteArtist")]
        public IHttpActionResult DeleteArtist(int id)
        {
            return Ok(objBLArtist.Delete(id));
        }
    }
}
