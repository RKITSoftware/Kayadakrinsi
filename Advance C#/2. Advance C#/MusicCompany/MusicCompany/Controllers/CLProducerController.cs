using System.Net.Http;
using System.Web.Http;
using MusicCompany.BasicAuth;
using MusicCompany.BusinessLogic;
using MusicCompany.Models;

namespace MusicCompany.Controllers
{
    [BasicAuthenticationAttribute]
    public class CLProducerController : ApiController
    {
        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/GetProducer")]
        public IHttpActionResult GetProducer()
        {
            return Ok(BLProducer.Select());
        }

        [HttpGet]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/GetProducerFile")]
        public HttpResponseMessage GetProducerFile()
        {
            return BLProducer.Download();
        }

        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/AddProducer")]
        public IHttpActionResult AddProducer(PRO01 objPRO01)
        {
            return Ok(BLProducer.Insert(objPRO01));
        }

        [HttpPost]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/WriteFile")]
        public IHttpActionResult WriteFile()
        {
            return Ok(BLProducer.WriteData());
        }

        [HttpPut]
        [BasicAuthorizationAttribute(Roles = "Producer,Admin")]
        [Route("api/CLProducer/EditProducer")]
        public IHttpActionResult EditProducer(PRO01 objPRO01)
        {
            return Ok(BLProducer.Update(objPRO01));
        }

        [HttpDelete]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        [Route("api/CLProducer/DeleteProducer")]
        public IHttpActionResult DeleteProducer(int id)
        {
            return Ok(BLProducer.Delete(id));
        }
    }
}