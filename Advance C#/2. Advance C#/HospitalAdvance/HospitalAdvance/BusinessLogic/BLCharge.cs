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
	public class BLCharge
	{
		#region Private Members

		/// <summary>
		/// Path of file in which charge data will be written
		/// </summary>
		private static readonly string path = HttpContext.Current.Server.MapPath("~/Charge") +
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
		static BLCharge()
		{
			_dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Insert charge
		/// </summary>
		/// <param name="objCRG01">object of CRG01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Insert(CRG01 objCRG01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<CRG01>())
				{
					db.CreateTable<CRG01>();
				}

				var dieases = db.SingleById<DIS01>(objCRG01.G01F03);
				var doctor = db.SingleById<STF01>(objCRG01.G01F02);

				if(dieases == null || doctor == null)
				{
					return ("Enter data with valid refrences");
				}

				db.Insert(objCRG01);

				return "Success!";
			}
		}

		/// <summary>
		/// Update charge
		/// </summary>
		/// <param name="objCRG01">object of CRG01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Update(CRG01 objCRG01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<CRG01>())
				{
					db.CreateTable<CRG01>();
					return "No records to be update!";
				}

				var dieases = db.SingleById<DIS01>(objCRG01.G01F03);
				var doctor = db.SingleById<STF01>(objCRG01.G01F02);

				if (dieases == null || doctor == null)
				{
					return ("Enter data with valid refrences");
				}

				db.Update(objCRG01,u=>u.G01F01==objCRG01.G01F01);
				return "Charge updated successfully!";
			}
		}

		/// <summary>
		/// Select data from CRG01
		/// </summary>
		/// <returns>Serialized string or appropriate message</returns>
		public List<CRG01> Select()
		{
			List<CRG01> lstCRG01 = new List<CRG01>();

			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<CRG01>())
				{
					db.CreateTable<CRG01>();
				}

				lstCRG01 = db.Select<CRG01>();

				BLUser.CacheOperations("Charges", lstCRG01);

				return lstCRG01;
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
				var lstCRG01 = Select();

				using(var db = _dbFactory.OpenDbConnection())
				{
					sw.WriteLine("Charge id, Doctor name, Dieases name, Amount");  
					foreach (var obj in lstCRG01)
					{
						var doctor = db.SingleById<STF01>(obj.G01F02).F01F02;
						var dieases = db.SingleById<DIS01>(obj.G01F03).S01F02;

						if(dieases != null && doctor != null)
						{
							sw.Write(obj.G01F01 + ", ");
							sw.Write(doctor + ", ");
							sw.Write(dieases + ", ");
							sw.Write(obj.G01F04);
							sw.WriteLine();
						}
					}
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
				FileName = "Downloaded-Charges" + Path.GetFileName(path)
			};

			return response;
		}

		#endregion
	}
}