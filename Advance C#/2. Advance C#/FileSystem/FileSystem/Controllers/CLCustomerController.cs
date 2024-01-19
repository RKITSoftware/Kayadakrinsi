using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using FileSystem.BusinessLogic;
using FileSystem.Models;

namespace FileSystem.Controllers
{
    public class CLCustomerController : ApiController
    {
        [HttpGet]
        [Route("api/CLCustomer/GetAllCustomers")]
        public IHttpActionResult GetAllCustomers()
        {
            return Ok(BLCustomer.GetCustomers());
        }

        [HttpGet]
        [Route("api/CLCustomer/GetCustomerById")]
        public IHttpActionResult GetCustomerById(int id)
        {
            return Ok(BLCustomer.GetCustomerById(id));
        }

        [HttpPost]
        [Route("api/CLCustomer/AddCustomer")]
        public IHttpActionResult AddCustomer(CUS01 objCUS01) 
        { 
            return Ok(BLCustomer.AddCustomer(objCUS01));
        }

        [HttpPut]
        [Route("api/CLCustomer/UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(CUS01 objCUS01)
        {
            return Ok(BLCustomer.UpdateCustomer(objCUS01));
        }

        [HttpDelete]
        [Route("api/CLCustomer/DeleteCustomer")]
        public IHttpActionResult DeleteCustomer(int id) 
        { 
            return Ok(BLCustomer.DeleteCustomer(id));
        }

        [HttpGet]
        [Route("api/CLCustomer/Write")]
        public IHttpActionResult Write()
        {
            return Ok(BLCustomer.WriteData());
        }

        [HttpGet]
        [Route("api/CLCustomer/Download")]
        public HttpResponseMessage Download()
        {
            return BLCustomer.Download();
        }

        [HttpPost]
        [Route("api/CLCustomer/Upload")]
        public HttpResponseMessage Upload()
        {
            var currentContext = HttpContext.Current;
            var server = currentContext.Server;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string fileName in httpRequest.Files.Keys)
                {
                    var file = httpRequest.Files[fileName];
                    var path = Path.Combine(server.MapPath("~/Uploads"), file.FileName);
                    file.SaveAs(path);
                    
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
