using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for stf01 controller
    /// </summary>
    public class BLPatient
	{
		#region Private Members

		/// <summary>
		/// Path of file in which patient data will be written
		/// </summary>
		private readonly string path = HttpContext.Current.Server.MapPath("~/Patient") +
											  "\\" + DateTime.Now.ToShortDateString() + ".txt";

		/// <summary>
		/// Declares Db factory instance
		/// </summary>
		private readonly IDbConnectionFactory _dbFactory;

		/// <summary>
		/// Declares object of class DL
		/// </summary>
		private DL _objDL;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes db factory instance
		/// </summary>
		public BLPatient()
		{
            _objDL = new DL();
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Insert patient
		/// </summary>
		/// <param name="objPTN01">object of PTN01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Insert(PTN01 objPTN01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<PTN01>())
				{
					db.CreateTable<PTN01>();
				}

				var dieases = db.SingleById<DIS01>(objPTN01.N01F04);
				var user = db.SingleById<USR01>(objPTN01.N01F05);
				var patient = db.Select<PTN01>().FirstOrDefault(x => x.N01F05 == objPTN01.N01F05);

				if (patient != null && patient == objPTN01)
				{
					return "User already exist";
				}
				else if (patient != null)
				{
					db.Update(objPTN01, u => u.N01F01 == objPTN01.N01F01);
					return "Success!";
				}

				if(dieases == null || user == null)
				{
					return "Enter data with valid refrences";
				}
				db.Insert(objPTN01);

				return "Success!";
			}
		}

		/// <summary>
		/// Update patient
		/// </summary>
		/// <param name="objPTN01">object of PTN01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Update(PTN01 objPTN01)
		{
			try
			{
				using (var db = _dbFactory.OpenDbConnection())
				{
					if (!db.TableExists<PTN01>())
					{
						db.CreateTable<PTN01>();
						return "No records to be updated!";
					}

					var dieases = db.SingleById<DIS01>(objPTN01.N01F04);
					var user = db.SingleById<USR01>(objPTN01.N01F05);

					if (dieases == null || user == null)
					{
						return "Enter data with valid refrences";
					}

					int rowsAffected = db.Update(objPTN01, u => u.N01F01 == objPTN01.N01F01);

					if (rowsAffected > 0)
						return "Success!";
					else
						return "No records updated!";
				}
			}
			catch (Exception ex)
			{
				// Log the exception or handle it appropriately
				return $"Error: {ex.Message}";
			}
		}


		/// <summary>
		/// Select data from PTN01
		/// </summary>
		/// <returns>Serialized string or appropriate message</returns>
		public List<PTN01> Select()
		{
			List<PTN01> lstPTN01 = new List<PTN01>();

			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<PTN01>())
				{
					db.CreateTable<PTN01>();
				}

				lstPTN01 = db.Select<PTN01>();

				BLUser.CacheOperations("Patients", lstPTN01);

				return lstPTN01;
			}
		}

        /// <summary>
        /// Generates patient's charges details
        /// </summary>
        /// <param name="user">Current user</param>
        /// <returns>List o object</returns>
        public dynamic GetMyRecipt(USR01 user)
        {
			return _objDL.GetMyRecipt(user);
        }

        /// <summary>
        /// Selects data from database and Writes data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        public string WriteData()
		{
			using (StreamWriter sw = new StreamWriter(path))
			{
				var lstPTN01 = Select();
				using (var db = _dbFactory.OpenDbConnection())
				{
					sw.WriteLine("Patient id, Patient name, Patient mobile number, Dieases name, User id, IsActive");
					foreach (var obj in lstPTN01)
			  		{
						var diease = db.SingleById<DIS01>(obj.N01F04).S01F02;
						if (diease != null)
						{
							sw.Write(obj.N01F01 + ", ");
							sw.Write(obj.N01F02 + ", ");
							sw.Write(obj.N01F03 + ", ");
							sw.Write(diease + ", ");
							sw.Write(obj.N01F05 + ", ");
							sw.Write(obj.N01F06);
							sw.WriteLine();
						}
					}
				}
			}
			return "File created successfully 🙌";
		}

		/// <summary>
		/// Selects data from database and Writes data into file of current user
		/// </summary>
		/// <returns>Appropriate Message</returns>
		public string WriteMyFile(USR01 user)
		{
			string newPath = path.Replace(DateTime.Now.ToShortDateString(), user.R01F02 + DateTime.Now.ToShortDateString());

			using (StreamWriter sw = new StreamWriter(newPath))
			{
				using (var db = _dbFactory.OpenDbConnection())
				{
					sw.WriteLine("Patient id, Patient name, Patient mobile number, Dieases name, User id, IsActive");
					var obj = Select().FirstOrDefault(x => x.N01F05 == user.R01F01);
					if(obj != null)
					{
						var diease = db.SingleById<DIS01>(obj.N01F04).S01F02;
						if (diease != null)
						{
							sw.Write(obj.N01F01 + ", ");
							sw.Write(obj.N01F02 + ", ");
							sw.Write(obj.N01F03 + ", ");
							sw.Write(diease + ", ");
							sw.Write(obj.N01F05 + ", ");
							sw.Write(obj.N01F06);
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
		public HttpResponseMessage DownloadMyFile(USR01 user)
		{
			var newPath = path.Replace(DateTime.Now.ToShortDateString(), user.R01F02 + DateTime.Now.ToShortDateString());
			// Check if the file exists
			if (!File.Exists(newPath))
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			// Create a response message
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

			// Read the file into a byte array
			byte[] fileBytes = File.ReadAllBytes(newPath);

			// Create a content stream from the byte array
			response.Content = new ByteArrayContent(fileBytes);

			// Set the content type based on the file extension
			response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

			// Set the content disposition header to force a download
			response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
			{
				FileName = "Downloaded-Patient-" + Path.GetFileName(newPath)
			};

			return response;
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
				FileName = "Downloaded-Patients" + Path.GetFileName(path)
			};

			return response;
		}

		#endregion
	}
}