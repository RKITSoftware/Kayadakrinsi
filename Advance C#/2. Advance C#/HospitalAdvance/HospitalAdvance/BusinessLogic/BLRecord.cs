using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spire.Xls;

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
		private readonly string path = HttpContext.Current.Server.MapPath("~/Record") +
											  "\\" + DateTime.Now.ToShortDateString() + ".xlsx";

		/// <summary>
		/// Declares Db factory instance
		/// </summary>
		private readonly IDbConnectionFactory _dbFactory;

		/// <summary>
		/// Declares object of DLRecord class
		/// </summary>
		private DL _objDL;

		#endregion

		#region Constructors

		/// <summary>
		/// Intializes connection objects
		/// </summary>
		public BLRecord()
		{
			_objDL = new DL();
			_dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
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

					var patient = db.SingleById<PTN01>(objRCD01.D01F02);
					var doctor = db.SingleById<STF01>(objRCD01.D01F03);
					var helper = db.SingleById<STF02>(objRCD01.D01F04);
					var dieases = db.SingleById<STF02>(objRCD01.D01F05);
					var charge = db.SingleById<CRG01>(objRCD01.D01F06);

					if(patient==null || doctor==null || helper==null || dieases==null || charge == null)
					{
						return "Enter data with valid references";
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
			if (objRCD01.D01F08 >= objRCD01.D01F07)
			{
				using (var db = _dbFactory.OpenDbConnection())
				{
					if (!db.TableExists<RCD01>())
					{
						db.CreateTable<RCD01>();
						return "No records to be update!";
					}

					var patient = db.SingleById<PTN01>(objRCD01.D01F02);
					var doctor = db.SingleById<STF01>(objRCD01.D01F03);
					var helper = db.SingleById<STF02>(objRCD01.D01F04);
					var dieases = db.SingleById<STF02>(objRCD01.D01F05);
					var charge = db.SingleById<CRG01>(objRCD01.D01F06);

					if (patient == null || doctor == null || helper == null || dieases == null || charge == null)
					{
						return "Enter data with valid references";
					}

					objRCD01.D01F09 = CalculateTotal(objRCD01);

					db.Update(objRCD01, u => u.D01F01 == objRCD01.D01F01);

					return "Record updated successfully!";
				}
			}
			return "Invalid data";
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
			return _objDL.SelectAllDetails();
		}

        /// <summary>
        /// Selects data from database and Writes data into .xlsx file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        public string WriteData()
		{
			//Create a Workbook object
			Workbook workbook = new Workbook();

			//Remove default worksheets
			workbook.Worksheets.Clear();

			//Add a worksheet and name it
			Worksheet worksheet = workbook.Worksheets.Add("Records");

			var lstRCD01 = _objDL.SelectAllDetails();

			//Write data to specific cells
			worksheet.Range["A1"].Value = "Record id";
			worksheet.Range[1, 2].Value = "Patient name";
			worksheet.Range[1, 3].Value = "Doctor name";
			worksheet.Range[1, 4].Value = "Helper name";
			worksheet.Range[1, 5].Value = "Dieases name";
			worksheet.Range[1, 6].Value = "Admit date";
			worksheet.Range[1, 7].Value = "Discharge date";
			worksheet.Range[1, 8].Value = "Total";

			int i = 2;

			foreach ( var obj in lstRCD01)
			{
				worksheet.Range[i, 1].Value = Convert.ToString(obj.RECORD_ID);
				worksheet.Range[i, 2].Value = obj.PATIENT_NAME;
				worksheet.Range[i, 3].Value = obj.DOCTOR_NAME;
				worksheet.Range[i, 4].Value = obj.HELPER_NAME;
				worksheet.Range[i, 5].Value = obj.DIEASES_NAME;
				worksheet.Range[i, 6].Value = Convert.ToString(obj.ADMIT_DATE);
				worksheet.Range[i, 7].Value = Convert.ToString(obj.DISCHARGE_DATE);
				worksheet.Range[i, 8].Value = Convert.ToString(obj.TOTAL);
				i++;
			}

			//Auto fit column width
			worksheet.AllocatedRange.AutoFitColumns();

			//Save to an Excel file
			workbook.SaveToFile(path,ExcelVersion.Version2013);

			return "File created successfully 🙌";
		}

		/// <summary>
		/// Download file
		/// </summary>
		/// <returns>HttpResponseMessage with file</returns>
		public HttpResponseMessage Download()
		{
			try
			{
				// Check if the file exists
				if (!File.Exists(path))
				{
					return new HttpResponseMessage(HttpStatusCode.NotFound);
				}

				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

				// Read the file content into a byte array
				byte[] fileBytes = File.ReadAllBytes(path);

				// Set the content of the response to the file content
				response.Content = new ByteArrayContent(fileBytes);

				// Set the content type for Excel files
				response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

				// Set the content disposition header to force a download
				response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
				{
					FileName = "Downloaded-Records" + Path.GetFileName(path) // Ensure the file name has the .xlsx extension
				};

				return response;
			}
			catch (Exception ex)
			{
				// Log the error
				Console.WriteLine($"An error occurred while downloading the file: {ex.Message}");
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}

		#endregion

	}
}