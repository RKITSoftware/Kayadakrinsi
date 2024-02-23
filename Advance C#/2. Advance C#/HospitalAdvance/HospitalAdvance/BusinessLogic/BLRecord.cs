using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
	/// Handles logic for controller
	/// </summary>
	public class BLRecord
	{
		#region Private Members

		/// <summary>
		/// Path of file in which record data will be written
		/// </summary>
		private static readonly string path = HttpContext.Current.Server.MapPath("~/Record") +
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
		/// Intializes connection objects
		/// </summary>
		static BLRecord()
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
		/// Calculates total based on number of days patient stayed
		/// </summary>
		/// <param name="objRCD01"></param>
		/// <returns>Total amount</returns>
		public decimal CalculateTotal(RCD01 objRCD01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<RCD01>())
				{
					db.CreateTable<RCD01>();
				}
				var charge = db.SingleById<CRG01>(objRCD01.D01F06).G01F04;
				var amount = (objRCD01.D01F08.Day - objRCD01.D01F07.Day) * charge;
				return amount;
			}
		}

		/// <summary>
		/// Insert record
		/// </summary>
		/// <param name="objRCD01">object of RCD01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Insert(RCD01 objRCD01)
		{
			if(objRCD01.D01F08 >= objRCD01.D01F07)
			{
				using (var db = _dbFactory.OpenDbConnection())
				{
					if (!db.TableExists<RCD01>())
					{
						db.CreateTable<RCD01>();
					}
					objRCD01.D01F09 = CalculateTotal(objRCD01);
					db.Insert(objRCD01);
					return "Success!";
				}
			}
			return "Invalid data";
		}

		/// <summary>
		/// Update record
		/// </summary>
		/// <param name="objRCD01">object of RCD01 class</param>
		/// <returns>Appropriate Message</returns>
		public string Update(RCD01 objRCD01)
		{
			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<RCD01>())
				{
					db.CreateTable<RCD01>();
					return "No records to be update!";
				}
				objRCD01.D01F09 = CalculateTotal(objRCD01);
				db.Update(objRCD01, u => u.D01F01 == objRCD01.D01F01);
				return "Record updated successfully!";
			}
		}


		/// <summary>
		/// Select data from RCD01
		/// </summary>
		/// <returns>Serialized string or appropriate message</returns>
		public List<RCD01> Select()
		{
			List<RCD01> lstRCD01 = new List<RCD01>();

			using (var db = _dbFactory.OpenDbConnection())
			{
				if (!db.TableExists<RCD01>())
				{
					db.CreateTable<RCD01>();
				}

				lstRCD01 = db.Select<RCD01>();

				BLUser.CacheOperations("Records", lstRCD01);

				return lstRCD01;
			}
		}

		/// <summary>
		/// Selects record of record with all details
		/// </summary>
		/// <returns>List of record details</returns>
		public dynamic SelectAllDetails()
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
				"D01.D01F06 = G01.G01F01 ORDER BY RECORD_ID";

			//List<object>[] lstDetails = new List<object>[6];
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
				var lstRCD01 = SelectAllDetails();
				sw.WriteLine("Record id, Patient name, Doctor name, Helper name, Dieases name, Admit date, Discharge date, Total");
				foreach (var obj in lstRCD01)
				{
					sw.Write(obj.RECORD_ID + ", ");
					sw.Write(obj.PATIENT_NAME + ", ");
					sw.Write(obj.HELPER_NAME + ", ");
					sw.Write(obj.DIEASES_NAME + ", ");
					sw.Write(obj.ADMIT_DATE + ", ");
					sw.Write(obj.DISCHARGE_DATE + ", ");
					sw.Write(obj.TOTAL);
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
				FileName = "Downloaded-Records" + Path.GetFileName(path)
			};

			return response;
		}

		#endregion
	}
}