using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.Controllers
{
    /// <summary>
    /// Inherits generic controller with type BOK01
    /// </summary>
    public class CLBookController : CLGenericController<BOK01>
    {
        /// <summary>
        /// Defines service interface type to BOK01
        /// </summary>
        /// <param name="service">object of interface service</param>
        public CLBookController(IServices<BOK01> service) : base(service)
        {
        }
    }
}