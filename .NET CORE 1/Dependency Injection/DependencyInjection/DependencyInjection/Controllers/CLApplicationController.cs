using DependencyInjection.Interfaces;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Handles request of application's operation using dependency injection concept
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLApplicationController : ControllerBase
    {
        /// <summary>
        /// Declares object of IOperations interface
        /// </summary>
        private readonly IOperations _operations;

        /// <summary>
        /// Declares object of ITransient interface
        /// </summary>
        private readonly ITransient _transient;

        /// <summary>
        /// Declares object of IScopped interface
        /// </summary>
        private readonly IScopped _scopped;

        /// <summary>
        /// Initializes instances of interfaces automatically from Built-in IOC container
        /// </summary>
        /// <param name="operations">Instance of IOperations interface</param>
        public CLApplicationController(IOperations operations, ITransient transient, IScopped scopped)
        {
            _operations = operations;
            _transient = transient;
            _scopped = scopped;
        }

        /// <summary>
        /// Handles request for getting all applications
        /// </summary>
        /// <returns>List of applications</returns>
        [HttpGet]
        [Route("GetApplications")]
        public IActionResult GetApplications()
        {
            return Ok(_operations.Select());
        }

        /// <summary>
        /// Handles request for adding new apllication
        /// </summary>
        /// <param name="objAPL01">Object of application to be add</param>
        /// <returns>Appropriate message</returns>
        [HttpPost]
        [Route("AddApplication")]
        public IActionResult AddApplication([FromBody]APL01 objAPL01)
        {
            return Ok(_operations.Add(objAPL01));
        }

        /// <summary>
        /// Handles request for update apllication
        /// </summary>
        /// <param name="objAPL01">Object of application to be update</param>
        /// <returns>Appropriate message</returns>
        [HttpPut]
        [Route("UpdateApplication")]
        public IActionResult UpdateApplication([FromBody] APL01 objAPL01)
        {
            var result = _operations.Update(objAPL01);
            if(result == null)
            {
                return NotFound("Invalid data to update");
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Handles request for deleting apllication
        /// </summary>
        /// <param name="id">Id of application to be delete</param>
        /// <returns>Appropriate message</returns>
        [HttpDelete]
        [Route("DeleteApplication")]
        public IActionResult DeleteApplication(int id)
        {
            var result = _operations.Delete(id);
            if (result == null)
            {
                return NotFound("Invalid data to update");
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Tests singleton service lifetime
        /// </summary>
        /// <param name="singletone">Instance of ISingletone interface</param>
        /// <returns>Guid</returns>
        [HttpGet]
        [Route("GetSingletone")]
        public IActionResult GetSingletone([FromServices] ISingletone singletone)
        {
            return Ok(singletone.Get());
        }

        /// <summary>
        /// Tests transient service lifetime
        /// </summary>
        /// <param name="transient">Instance of ITransient interface</param>
        /// <returns>Guid</returns>
        [HttpGet]
        [Route("GetTransient")]
        public IActionResult GetTransient([FromServices] ITransient transient)
        {
            Guid guid1 = transient.Get();
            Guid guid2 = _transient.Get();

            return Ok(new { Guid1 = guid1, Guid2 = guid2});
        }

        /// <summary>
        /// Tests scopped service lifetime
        /// </summary>
        /// <param name="scopped">Instance of IScopped interface</param>
        /// <returns>Guid</returns>
        [HttpGet]
        [Route("GetScopped")]
        public IActionResult GetScopped([FromServices] IScopped scopped)
        {
            Guid guid1 = scopped.Get();
            Guid guid2 = _scopped.Get();

            return Ok(new { Guid1 = guid1, Guid2 = guid2 });
        }

    }
} 
