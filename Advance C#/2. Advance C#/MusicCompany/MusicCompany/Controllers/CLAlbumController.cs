﻿using System.Net.Http;
using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    /// <summary>
    /// Handles methods to predorm operations related to album
    /// </summary>
    [BasicAuthenticationAttribute]
    public class CLAlbumController : ApiController
    {
        /// <summary>
        /// Handles get reuest for getting album data
        /// </summary>
        /// <returns>All data from table ALB01</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/GetAlbum")]
        public IHttpActionResult GetAlbum()
        {
            return Ok(BLAlbum.Select());
        }

        /// <summary>
        /// Downloads file of ablum data
        /// </summary>
        /// <returns>Downloaded text file</returns>
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/GetAlbumFile")]
        public HttpResponseMessage GetAlbumFile()
        {
            return BLAlbum.Download();
        }

        /// <summary>
        /// Adds album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class to be added</param>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/AddAlbum")]
        public IHttpActionResult AddAlbum(ALB01 objALB01)
        {
            return Ok(BLAlbum.Insert(objALB01));
        }

        /// <summary>
        /// Write data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Album,Producer,Admin")]
        [Route("api/CLAlbum/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(BLAlbum.WriteData());
        }

        /// <summary>
        /// Edit album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class to be edit</param>
        /// <returns>Appropriate Message</returns>
        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Artist,Producer,Admin")]
        [Route("api/CLAlbum/EditAlbum")]
        public IHttpActionResult EditAlbum(ALB01 objALB01)
        {
            return Ok(BLAlbum.Update(objALB01));
        }

        /// <summary>
        /// Deletes album
        /// </summary>
        /// <param name="id">id of album to be deleted</param>
        /// <returns>Appropriate Message</returns>
        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLAlbum/DeleteAlbum")]
        public IHttpActionResult DeleteAlbum(int id)
        {
            return Ok(BLAlbum.Delete(id));
        }
    }
}