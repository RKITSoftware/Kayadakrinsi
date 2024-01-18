using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.Controllers
{
    public class CLAlbumController : CLGenericController<ALB01>
    {
        public CLAlbumController(IServices<ALB01> service) : base(service)
        {
        }
    }
}
