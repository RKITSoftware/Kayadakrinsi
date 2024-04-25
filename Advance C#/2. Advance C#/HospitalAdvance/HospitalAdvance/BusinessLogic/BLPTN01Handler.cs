using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for CLPTN01 controller
    /// </summary>
    public class BLPTN01Handler
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

        #endregion

        #region Public Members

        /// <summary>
        /// Instance of operation type
        /// </summary>
        public enmOperations ObjOperations { get; set; }

        /// <summary>
        /// Instance of PTN01 class
        /// </summary>
        public PTN01 ObjPTN01 { get; set; }

        /// <summary>
        /// Instance of BLUSR01Handler class
        /// </summary>
        public BLUSR01Handler objBLUSR01Handler = new BLUSR01Handler();

        /// <summary>
        /// Instance of BLDIS01Handler class
        /// </summary>
        public BLDIS01Handler objBLDIS01Handler = new BLDIS01Handler();

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        /// <summary>
        /// Instance of DBPTN01Context DB context class
        /// </summary>
        public DBPTN01Context objDBPTN01Context = new DBPTN01Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLPTN01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTOPTN01">DTOPTN01 object to preprocess</param>
        public void PreSave(DTOPTN01 objDTOPTN01)
        {
            ObjPTN01 = objDTOPTN01.Map<DTOPTN01, PTN01>();
        }

        /// <summary>
        /// Checks weather object of PTN01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<PTN01>()
                                    .Where(u => u.N01F01 == id);

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

            string mobilePattern = "^[0-9]{10}$";

            if (!objBLDIS01Handler.IsExist(ObjPTN01.N01F04))
            {
                response.isError = true;
                response.message = "Invalid dieases";
            }
            else if (!Regex.IsMatch(ObjPTN01.N01F03.ToString(), mobilePattern))
            {
                response.isError = true;
                response.message = "Invalid Contact number";
            }
            else if (ObjOperations == enmOperations.I)
            {
                if (IsExist(ObjPTN01.N01F01))
                {
                    response.isError = true;
                    response.message = "Patient already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjPTN01.N01F01))
                {
                    response.isError = true;
                    response.message = "No Patient found";
                }
            }

            return response;
        }

        /// <summary>
        /// Saves the PTN01 object
        /// </summary>
        /// <returns>Response indicating the result of the save operation</returns>
        public Response Save()
        {
            Response response = new Response();

            objBLUSR01Handler.ObjOperations = ObjOperations;

            using (var db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.OpenTransaction(IsolationLevel.Serializable))
                {
                    try
                    {

                        // Insert record into STF01 table

                        if (ObjOperations == enmOperations.I)
                        {
                            int ptn01Id = (int)db.Insert<PTN01>(ObjPTN01, selectIdentity: true);

                            objBLUSR01Handler.PreSave(ObjPTN01.N01F02, enmUserRole.P, ptn01Id);

                            response = objBLUSR01Handler.Validation();
                            if (!response.isError)
                            {
                                db.InsertOnly<USR01>(objBLUSR01Handler.ObjUSR01,
                                    x => new { x.R01F02, x.R01F03, x.R01F04, x.R01F08 });

                                response.message = "Patient Inserted successfully";
                                transaction.Commit();
                            }
                        }
                        else if (ObjOperations == enmOperations.U)
                        {
                            db.Update(ObjPTN01);
                            response.message = "Patient Updated successfully";
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Retrieves all PTN01 objects
        /// </summary>
        /// <returns>Response containing all PTN01 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objDBPTN01Context.Select();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validates the object before deleting
        /// </summary>
        /// <param name="id">Id of patient to be delete</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();
            if (!IsExist(id))
            {
                response.isError = true;
                response.message = "Patient Not Found";
            }
            return response;
        }

        /// <summary>
        /// Deletes PTN01 object
        /// </summary>
        /// <param name="id">Id of patient to be delete</param>
        /// <returns>Response indicating the result of the delete operation</returns>
        public Response Delete(int id)
        {
            Response response = new Response();

            using (var db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.OpenTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        db.UpdateOnly<USR01>(() => new USR01 { R01F05 = enmIsActive.N, R01F06 = null }, where: u => u.R01F06 == id);

                        db.DeleteById<PTN01>(id);

                        transaction.Commit();

                        response.message = "Patient Deleted successfully";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback the transaction in case of exception
                        throw ex;
                    }
                }

                return response;
            }
        }

        /// <summary>
        /// Generates patient's receipt
        /// </summary>
        /// <param name="user">Current user</param>
        /// <returns>List of object</returns>
        public dynamic GetMyRecipt(USR01 user)
        {
            return objDBPTN01Context.GetMyRecipt(user);
        }

        /// <summary>
        /// Selects data from database and Writes data into file
        /// </summary>
        public void WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                DataTable dataTable = objDBPTN01Context.Select();

                sw.WriteLine("Patient id, Patient name, Patient mobile number, Dieases name");

                foreach (DataRow row in dataTable.Rows)
                {
                    int columnCount = 0;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        if (columnCount == dataTable.Columns.Count - 1)
                        {
                            sw.WriteLine(string.Format(@"{0}"
                                                        , row[col]));
                            columnCount = 0;
                        }
                        else
                        {
                            sw.Write(string.Format(@"{0},"
                                                        , row[col]));
                            columnCount++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Selects data from database and Writes data into file of current user
        /// </summary>
        public void WriteMyFile(USR01 user)
        {
            PTN01 objPTN01 = objDBPTN01Context.SelectList().FirstOrDefault(p => p.N01F01 == user.R01F08);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objPTN01.N01F02 + DateTime.Now.ToShortDateString());

            DataTable dataTable = objDBPTN01Context.Select();

            DataRow row = dataTable.Rows[objPTN01.N01F01 - 1];

            using (StreamWriter sw = new StreamWriter(newPath))
            {
                sw.WriteLine("Patient id, Patient name, Patient mobile number, Dieases name");

                int columnCount = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (columnCount == dataTable.Columns.Count - 1)
                    {
                        sw.WriteLine(string.Format(@"{0}"
                                                    , row[col]));
                        columnCount = 0;
                    }
                    else
                    {
                        sw.Write(string.Format(@"{0},"
                                                    , row[col]));
                        columnCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Downloads current logged in patient's file
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public HttpResponseMessage DownloadMyFile(USR01 user)
        {
            WriteMyFile(user);

            PTN01 objPTN01 = objDBPTN01Context.SelectList().FirstOrDefault(p => p.N01F01 == user.R01F08);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objPTN01.N01F02 + DateTime.Now.ToShortDateString());

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
        /// Downloads file containg all patients data
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public HttpResponseMessage Download()
        {
            WriteData();

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