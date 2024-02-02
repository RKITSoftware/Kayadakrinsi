using System.Web.Http;
using GenericControllers.Interface;

namespace GenericControllers.Controllers
{

    /// <summary>
    /// Generic controller
    /// </summary>
    /// <typeparam name="T">class type</typeparam>
    public class CLGenericController<T> : ApiController where T : class
    {
        /// <summary>
        /// Declares object of IServices interface
        /// </summary>
        public IServices<T> _service;

        /// <summary>
        /// Initializes object of IServices interface
        /// </summary>
        public CLGenericController(IServices<T> service) {
            _service = service;
        }

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns>List of elements</returns>
        [HttpGet]
        public IHttpActionResult GetAllData()
        {
            return Ok(_service.GetElemets());
        }

        /// <summary>
        /// Get one element by id
        /// </summary>
        /// <param name="id">id of element</param>
        /// <returns>Element</returns>
        public IHttpActionResult GetById(int id) {
            return Ok(_service.GetElementById(id));
        }

        /// <summary>
        /// Adds element to the list
        /// </summary>
        /// <param name="element">object of element which will be added</param>
        /// <returns>List of elements</returns>
        [HttpPost]
        public IHttpActionResult AddData(T element) {
            return Ok(_service.AddElement(element));
        }

        /// <summary>
        /// Edits element inside the list
        /// </summary>
        /// <param name="element">object of element which will be edit</param>
        /// <returns>List of elements</returns>
        [HttpPut]
        public IHttpActionResult UpdateData(T element)
        {
            return Ok(_service.UpdateElement(element));
        }

        /// <summary>
        /// Deletes element from list
        /// </summary>
        /// <param name="id">id of element which will be deleted</param>
        /// <returns>List of elements</returns>
        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            return Ok(_service.RemoveElement(id));
        }
    }
}