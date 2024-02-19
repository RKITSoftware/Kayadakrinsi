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
        /// <summary>
        /// Declares object of cache class
        /// </summary>
        Cache objCache;

		/// <summary>
		/// Defines object of cache class
		/// </summary>
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
            HttpContext.Current.Cache.Insert("states", "data", null, System.DateTime.Now.AddMinutes(20),Cache.NoSlidingExpiration);
            objCache.Insert("states", BLData.Data());
            objCache.Insert("Name", BLData.Data());
            return Ok(BLData.Data());
        }

		/// <summary>
		/// Get data from cache
		/// </summary>
		/// <returns>Cache</returns>
		[HttpGet]
        [Route("api/countCache")]
        public IHttpActionResult GetCache() 
        {
            return Ok(new { Count = objCache.Count, Data = objCache.Get("states"), context = objCache.Get("states"), context_cache = HttpContext.Current.Cache.Get("states") });
        }

		/// <summary>
		/// Remove from cache
		/// </summary>
		/// <returns>Count of cache items after data removed</returns>
		[HttpDelete]
        [Route("api/countAfterRemove")]
        public IHttpActionResult Remove() 
        { 
            objCache.Remove("Name");
            return Ok(objCache.Count);
        }
    }
}
