using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace HTTP_Caching.Controllers
{
    /// <summary>
    /// Defines custom controller
    /// </summary>
    public class CLDataController : ApiController
    {
        Cache objCache;

        public CLDataController() 
        { 
           objCache  = new Cache();
        }

        /// <summary>
        /// Returns dummy data defined in it with the help of dictionary
        /// </summary>
        [HttpGet]
        [AllowAnonymous] // Anyone can access this cache
        [Route("GetData")] // Route is changed to GetData instead of api/["controller"]
        /*[BLCacheFilter(TimeDuration = 100)]*/ // Using custom cache filter made previously with it's parameter
        public IHttpActionResult getData()
        {
            HttpContext.Current.Cache.Insert("states", BLData.Data(), null, System.DateTime.Now.AddMinutes(20),Cache.NoSlidingExpiration);
            objCache.Insert("states", BLData.Data());
            objCache.Insert("Name", BLData.Data());
            return Ok(BLData.Data());
        }

        [HttpGet]
        [Route("api/countCache")]
        public IHttpActionResult Count() 
        {
            return Ok(objCache.Count);
        }


        [HttpDelete]
        [Route("api/countAfterRemove")]
        public IHttpActionResult Remove() 
        { 
            objCache.Remove("Name");
            return Ok(objCache.Count);
        }
    }
}
