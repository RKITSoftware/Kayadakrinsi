using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.Models;
using MySql.Data.MySqlClient;
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
		private static readonly string path = HttpContext.Current.Server.MapPath("~/Patient") +
											  "\\" + DateTime.Now.ToShortDateString() + ".txt";

		/// <summary>
		/// Declares Db factory instance
		/// </summary>
		private static readonly IDbConnectionFactory _dbFactory;

		/// <summary>
		/// Connection string
		/// </summary>
		private static readonly string _connectionString;

		/// <summary>
		/// Connection object of class MySqlConnection
		/// </summary>
		private static readonly MySqlConnection _connection;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes db factory instance
		/// </summary>
		static BLPatient()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			_connection = new MySqlConnection(_connectionString);
			_dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// open connection to database
		/// </summary>
		/// <returns>true if connection opened else false</returns>
		private static bool OpenConnection()
		{
			try
			{
				_connection.Open();
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// close connection
		/// </summary>
		/// <returns>true if connection closed else false</returns>
		private static bool CloseConnection()
		{
			try
			{
				_connection.Close();
				return true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
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
			string query = "SELECT " +
				"D01.D01F01 AS RECORD_ID," +
				"N01.N01F02 AS PATIENT_NAME," +
				"F01.F01F02 AS DOCTOR_NAME," +
				"F02.F02F02 AS HELPER_NAME," +
				"S01.S01F02 AS DIEASES_NAME," +
				"D01.D01F07 AS ADMIT_DATE," +
				"D01.D01F08 AS DISCHARGE_DATE," +
				"D01.D01F09 AS TOTAL " +
				"FROM " +
				"RCD01 D01," +
				"PTN01 N01," +
				"STF01 F01," +
				"STF02 F02," +
				"DIS01 S01," +
				"CRG01 G01 " +
				"WHERE " +
				"D01.D01F02 = N01.N01F01 AND " +
				"D01.D01F03 = F01.F01F01 AND " +
				"D01.D01F04 = F02.F02F01 AND " +
				"D01.D01F05 = S01.S01F01 AND " +
				"D01.D01F06 = G01.G01F01 AND N01.N01F05 = " + user.R01F01;

			List<object> lstDetail = new List<object>();

			CloseConnection();
			//Open connection
			if (OpenConnection() == true)
			{
				//Create Command
				MySqlCommand command = new MySqlCommand(query, _connection);
				//Create a data reader and Execute the command
				MySqlDataReader dataReader = command.ExecuteReader();


				while (dataReader.Read())
				{
					lstDetail.Add(new
					{
						RECORD_ID = dataReader[0],
						PATIENT_NAME = dataReader[1],
						DOCTOR_NAME = dataReader[2],
						HELPER_NAME = dataReader[3],
						DIEASES_NAME = dataReader[4],
						ADMIT_DATE = dataReader[5],
						DISCHARGE_DATE = dataReader[6],
						TOTAL = dataReader[7]
					});
				}

				dataReader.Close();

				BLUser.CacheOperations("DetailedRecords", lstDetail);

				//close Connection
				CloseConnection();
			}
			return lstDetail;
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