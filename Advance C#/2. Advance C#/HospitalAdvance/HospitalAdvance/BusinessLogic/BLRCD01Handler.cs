using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spire.Xls;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for CLRCD01 controller
    /// </summary>
    public class BLRCD01Handler
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

        #endregion

        #region Public Members

        /// <summary>
        /// Instance of operation type
        /// </summary>
        public enmOperations ObjOperations { get; set; }

        /// <summary>
        /// Instance of RCD01 class
        /// </summary>
        public RCD01 ObjRCD01 { get; set; }

        /// <summary>
        /// Instance of BLPTN01Handler class
        /// </summary>
        public BLPTN01Handler objBLPTN01Handler = new BLPTN01Handler();

        /// <summary>
        /// Instance of BLSTF01Handler class
        /// </summary>
        public BLSTF01Handler objBLSTF01Handler = new BLSTF01Handler();

        /// <summary>
        /// Instance of BLSTF02Handler class
        /// </summary>
        public BLSTF02Handler objBLSTF02Handler = new BLSTF02Handler();

        /// <summary>
        /// Instance of DBDIS01Context DB context class
        /// </summary>
        public DBDIS01Context objDBDIS01Context = new DBDIS01Context();

        /// <summary>
        /// Instance of DBPTN01Context DB context class
        /// </summary>
        public DBPTN01Context objDBPTN01Context = new DBPTN01Context();

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        /// <summary>
        /// Instance of DBRCD01Context DB context class
        /// </summary>
        public DBRCD01Context objDBRCD01Context = new DBRCD01Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db Factory instance
        /// </summary>
        public BLRCD01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validates object before Presave
        /// </summary>
        /// <param name="objDTORCD01">object of class DTORCD01</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response PreValidation(DTORCD01 objDTORCD01)
        {
            Response response = new Response();

            if (objDTORCD01.D01F05 >= objDTORCD01.D01F06)
            {
                response.isError = true;
                response.message = "Invalid discharge date";
            }

            return response;
        }

        /// <summary>
        /// Calculates total amount of charges
        /// </summary>
        /// <returns>Total amount's value</returns>
        public double CalculateCharge()
        {
            int daysDifference;

            PTN01 objPTN01 = objDBPTN01Context.SelectList().FirstOrDefault(p => p.N01F01 == ObjRCD01.D01F02);

            double charge = objDBDIS01Context.SelectList().FirstOrDefault(d => d.S01F01 == objPTN01.N01F04).S01F03;

            TimeSpan timeDifference = ObjRCD01.D01F06 - ObjRCD01.D01F05;

            if (timeDifference.TotalDays > 0 && timeDifference.TotalDays < 1)
            {
                daysDifference = 1;
            }
            else
            {
                daysDifference = (int)timeDifference.TotalDays;
            }

            return daysDifference * charge;
        }

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTORCD01">object of class DTORCD01</param>
        public void PreSave(DTORCD01 objDTORCD01)
        {
            ObjRCD01 = objDTORCD01.Map<DTORCD01, RCD01>();

            ObjRCD01.D01F07 = CalculateCharge();
        }

        /// <summary>
        /// Checks weather object of RCD01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<RCD01>()
                                    .Where(u => u.D01F01 == id);

                // Execute the query and check if any records match the criteria
                return db.Exists(expression);
            }
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            if (!objBLPTN01Handler.IsExist(ObjRCD01.D01F02))
            {
                response.isError = true;
                response.message = "Invalid Patient Id";
            }
            else if (!objBLSTF01Handler.IsExist(ObjRCD01.D01F03))
            {
                response.isError = true;
                response.message = "Invalid Doctor Id";
            }
            else if (!objBLSTF02Handler.IsExist(ObjRCD01.D01F04))
            {
                response.isError = true;
                response.message = "Invalid Helper Id";
            }
            else if (objDBRCD01Context.getDischargeDate(ObjRCD01.D01F02) > ObjRCD01.D01F05)
            {
                response.isError = true;
                response.message = "Patient not discharged yet";
            }
            else if (ObjOperations == enmOperations.I)
            {
                if (IsExist(ObjRCD01.D01F01))
                {
                    response.isError = true;
                    response.message = "Record Already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjRCD01.D01F01))
                {
                    response.isError = true;
                    response.message = "Record Not Found";
                }
            }
            return response;
        }

        /// <summary>
        /// Saves the RCD01 object
        /// </summary>
        /// <returns>Response indicating the result of the save operation</returns>
        public Response Save()
        {
            Response response = new Response();

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (ObjOperations == enmOperations.I)
                    {
                        db.Insert(ObjRCD01);
                        response.message = "Record Inserted successfully";
                    }
                    else
                    {
                        db.Update(ObjRCD01);
                        response.message = "Record Updated successfully";
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects record of record with all details
        /// </summary>
        /// <returns>Response containing all record details</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objBLCommonHandler.ToDatatable(objDBRCD01Context.SelectAllDetails());

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects data from database and Writes data into .xlsx file
        /// </summary>
        public void WriteData()
        {
            //Create a Workbook object
            Workbook workbook = new Workbook();

            //Remove default worksheets
            workbook.Worksheets.Clear();

            //Add a worksheet and name it
            Worksheet worksheet = workbook.Worksheets.Add("Records");

            var lstRCD01 = objDBRCD01Context.SelectAllDetails();

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

            foreach (var obj in lstRCD01)
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
            workbook.SaveToFile(path, ExcelVersion.Version2013);
        }

        /// <summary>
        /// Downloads file containing all records
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public HttpResponseMessage Download()
        {
            try
            {
                WriteData();

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