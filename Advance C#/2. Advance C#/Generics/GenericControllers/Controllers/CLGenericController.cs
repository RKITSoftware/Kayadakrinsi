using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GenericControllers.Interface;

namespace GenericControllers.Controllers
{
    public class CLGenericController<T> : ApiController where T : class
    {
        public IServices<T> _service;
        public CLGenericController(IServices<T> service) {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetAllData()
        {
            return Ok(_service.GetElemets());
        }

        public IHttpActionResult GetById(int id) {
            return Ok(_service.GetElementById(id));
        }

        [HttpPost]
        public IHttpActionResult AddData(T element) {
            return Ok(_service.AddElement(element));
        }

        [HttpPut]
        public IHttpActionResult UpdateData(T element)
        {
            return Ok(_service.UpdateElement(element));
        }

        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            return Ok(_service.RemoveElement(id));
        }
    }
}