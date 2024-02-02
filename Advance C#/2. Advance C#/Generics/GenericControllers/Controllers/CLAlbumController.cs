using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.Controllers
{
    /// <summary>
    /// Inherits generic controller with type ALB01
    /// </summary>
    public class CLAlbumController : CLGenericController<ALB01>
    {
        /// <summary>
        /// Defines service interface type to ALB01
        /// </summary>
        /// <param name="albservice">object of interface service</param>
        public CLAlbumController(IServices<ALB01> service) : base(service)
        {
        }
    }
}
