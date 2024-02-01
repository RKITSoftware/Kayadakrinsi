using GenericControllers.Interface;
using GenericControllers.Models;

namespace GenericControllers.Controllers
{
    public class CLBookController : CLGenericController<BOK01>
    {
        public CLBookController(IServices<BOK01> service) : base(service)
        {
        }

        //[HttpGet]
        //[Route("api/CLBook/GetBooks")]
        //public IHttpActionResult GetBooks()
        //{
        //    return Ok(base.GetAllData());
        //}

        //[Route("api/CLBook/GetBookById")]
        //public IHttpActionResult GetBookByID(int id)
        //{
        //    return Ok(base.GetById(id));
        //}

        //[HttpPost]
        //[Route("api/CLBook/AddBook")]
        //public IHttpActionResult AddBook(BOK01 book)
        //{
        //    return Ok(base.AddData(book));
        //}

        //[HttpPut]
        //[Route("api/CLBook/UpdateBook")]
        //public IHttpActionResult UpdateBook(BOK01 book) 
        //{ 
        //    return Ok(base.UpdateData(book));
        //}

        //[HttpDelete]
        //[Route("api/CLBook/DeleteBook")]
        //public IHttpActionResult DeleteBook(int id)
        //{
        //    return Ok(base.DeleteById(id));
        //}
    }
}