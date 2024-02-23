using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
	/// <summary>
	/// Handles logic for controller
	/// </summary>
	public class BLDieases
	{
		#region Private Members

		/// <summary>
		/// Path of file in which dieases data will be written
		/// </summary>
		private static readonly string path = HttpContext.Current.Server.MapPath("~/Dieases") +
											  "\\" + DateTime.Now.ToShortDateString() + ".txt";

		/// <summary>
		/// Declares Db factory instance
		/// </summary>
		private static readonly IDbConnectionFactory _dbFactory;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes db factory instance
		/// </summary>
		static BLDieases()
		{
			_dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Insert dieases
		/// </summary>
		/// <param name="objDIS01">object of DIS01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Insert(DIS01 objDIS01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<DIS01>())
				{
					db.CreateTable<DIS01>();
				}
				db.Insert(objDIS01);
				return "Success!";
			}
		}

		/// <summary>
		/// Update dieases
		/// </summary>
		/// <param name="objDIS01">object of DIS01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Update(DIS01 objDIS01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<DIS01>())
				{
					db.CreateTable<DIS01>();
					return "No records to be update!";
				}
				db.Update(objDIS01,u=>u.S01F01==objDIS01.S01F01);
				return "Dieases updated successfully!";
			}
		}


		/// <summary>
		/// Select data from DIS01
		/// </summary>
		/// <returns>Serialized string or appropriate message</returns>
		public List<DIS01> Select()
		{
			List<DIS01> lstDIS01 = new List<DIS01>();

			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<DIS01>())
				{
					db.CreateTable<DIS01>();
				}

				lstDIS01 = db.Select<DIS01>();

				BLUser.CacheOperations("Dieases", lstDIS01);

				return lstDIS01;
			}
		}

		/// <summary>
		/// Selects data from database and Writes data into file
		/// </summary>
		/// <returns>Appropriate Message</returns>
		public string WriteData()
		{
			using (StreamWriter sw = new StreamWriter(path))
			{
				var lstDIS01 = Select();
				sw.WriteLine("Dieases id, Dieases name, Doctor id");
				foreach (var obj in lstDIS01)
				{
					sw.Write(obj.S01F01 + ", ");
					sw.Write(obj.S01F02 + ", ");
					sw.Write(obj.S01F03);
					sw.WriteLine();
				}
			}
			return "File created successfully 🙌";
		}

		/// <summary>
		/// Download file
		/// </summary>
		/// <returns>HttpResponseMessage with file</returns>
		public HttpResponseMessage Download()
		{
			// Check if the file exists
			if (!File.Exists(path))
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			// Create a response message
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

			// Read the file into a byte array
			byte[] fileBytes = File.ReadAllBytes(path);

			// Create a content stream from the byte array
			response.Content = new ByteArrayContent(fileBytes);

			// Set the content type based on the file extension
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

			// Set the content disposition header to force a download
			response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
			{
				FileName = "Downloaded-Dieases" + Path.GetFileName(path)
			};

			return response;
		}

		#endregion
	}
}