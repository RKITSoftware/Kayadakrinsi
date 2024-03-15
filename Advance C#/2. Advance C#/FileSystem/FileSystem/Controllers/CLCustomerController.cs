using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FileSystem.BusinessLogic;
using FileSystem.Models;

namespace FileSystem.Controllers
{
    /// <summary>
    /// Handles HTTP request for customers
    /// </summary>
    public class CLCustomerController : ApiController
	{
		/// <summary>
		/// Decalres object of clas BLCustomer
		/// </summary>
		public BLCustomer objBLCustomer;

		/// <summary>
		/// Defines object of class BLCustomer
		/// </summary>
		public CLCustomerController()
		{
			objBLCustomer = new BLCustomer();
		}

		/// <summary>
		/// Handles get request for getting all customer's data
		/// </summary>
		/// <returns>List of customers</returns>
		[HttpGet]
		[Route("api/CLCustomer/GetAllCustomers")]
		public IHttpActionResult GetAllCustomers()
		{
			return Ok(BLCustomer.lstCustomers);
		}

		/// <summary>
		/// Handles get request for getting one customer's data
		/// </summary>
		/// <param name="id">id of customer whoose data you want to get</param>
		/// <returns>Object of class CUS01</returns>
		[HttpGet]
		[Route("api/CLCustomer/GetCustomerById")]
		public IHttpActionResult GetCustomerById(int id)
		{
			return Ok(objBLCustomer.GetCustomerById(id));
		}

		/// <summary>
		/// Handles post request for adding customer data
		/// </summary>
		/// <param name="objCUS01">object of CUS01 that will be added</param>
		/// <returns>List of customers</returns>
		[HttpPost]
		[Route("api/CLCustomer/AddCustomer")]
		public IHttpActionResult AddCustomer(CUS01 objCUS01)
		{
			return Ok(objBLCustomer.AddCustomer(objCUS01));
		}

		/// <summary>
		/// Handles put request for editing customer data
		/// </summary>
		/// <param name="objCUS01">object of CUS01 that will be edit</param>
		/// <returns>List of customers</returns>
		[HttpPut]
		[Route("api/CLCustomer/UpdateCustomer")]
		public IHttpActionResult UpdateCustomer(CUS01 objCUS01)
		{
			return Ok(objBLCustomer.UpdateCustomer(objCUS01));
		}

		/// <summary>
		/// Handles delete request for deleting customer's data
		/// </summary>
		/// <param name="id">id of customer data which will be deleted</param>
		/// <returns>List of customers</returns>
		[HttpDelete]
		[Route("api/CLCustomer/DeleteCustomer")]
		public IHttpActionResult DeleteCustomer(int id)
		{
			return Ok(objBLCustomer.DeleteCustomer(id));
		}

		/// <summary>
		/// Writes customer's data into file
		/// </summary>
		/// <returns>Appropriate message</returns>
		[HttpPost]
		[Route("api/CLCustomer/Write")]
		public IHttpActionResult Write()
		{
			return Ok(objBLCustomer.WriteData());
		}

		/// <summary>
		/// Downloads file of customer's data
		/// </summary>
		/// <returns>File</returns>
		[HttpGet]
		[Route("api/CLCustomer/Download")]
		public HttpResponseMessage Download()
		{
			return objBLCustomer.Download();
		}

		/// <summary>
		/// Uploads a file
		/// </summary>
		/// <returns>Appropriate message</returns>
		[HttpPost]
		[Route("api/CLCustomer/Upload")]
		public async Task<IHttpActionResult> Upload()
		{
			var currentContext = HttpContext.Current;
			var httpRequest = currentContext.Request;
			var server = currentContext.Server;
			var root = server.MapPath("~/Uploads");

			var provider = new MultipartFormDataStreamProvider(root);

			try
			{
				await Request.Content.ReadAsMultipartAsync(provider);

				if (httpRequest.Files.Count > 0)
				{
					foreach (var file in provider.FileData)
					{
						var fileName = file.Headers.ContentDisposition.FileName.Trim('"');
						var localFileName = file.LocalFileName;

						var destinationPath = Path.Combine(root, fileName);

						// Check if the file already exists at the destination
						//if (File.Exists(destinationPath))
						//{
						//    File.Delete(localFileName);
						//    return BadRequest("A file with the same name already exists.");
						//}

						if (File.Exists(destinationPath))
						{
							// Rename the file to avoid overwriting
							var uniqueFileName = objBLCustomer.GetUniqueFileName(root, fileName);
							destinationPath = Path.Combine(root, uniqueFileName);
						}

						// Move the file from the temporary location to the desired location (doesn't cut file from original location)
						// Copy file saves file with original name and body_part....  
						File.Move(localFileName, destinationPath);

					}

					return Ok("File(s) uploaded successfully");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return BadRequest();
		}
	}
}
