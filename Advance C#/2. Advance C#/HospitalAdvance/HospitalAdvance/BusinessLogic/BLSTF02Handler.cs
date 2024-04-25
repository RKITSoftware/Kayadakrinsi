using System;
using System.Data;
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

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLSTF02 controller
    /// </summary>
    public class BLSTF02Handler
    {

        #region Private Members

        /// <summary>
        /// Path of file in which helper data will be written
        /// </summary>
        private readonly string path = HttpContext.Current.Server.MapPath("~/Helper") +
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
        /// Instance of STF02 class
        /// </summary>
        public STF02 ObjSTF02 { get; set; }


        /// <summary>
        /// Instance of BLUSR01Handler class
        /// </summary>
        public BLUSR01Handler objBLUSR01Handler = new BLUSR01Handler();

        /// <summary>
        /// Instance DBSTF02Context of DB context class
        /// </summary>
        public DBSTF02Context objDBSTF02Context = new DBSTF02Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLSTF02Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTOSTF02">DTOSTF02 object to preprocess</param>
        public void PreSave(DTOSTF02 objDTOSTF02)
        {
            ObjSTF02 = objDTOSTF02.Map<DTOSTF02, STF02>();
        }

        /// <summary>
        /// Checks weather object of STF02 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<STF02>()
                                    .Where(u => u.F02F01 == id);

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

            if (ObjOperations == enmOperations.I)
            {
                if (IsExist(ObjSTF02.F02F01))
                {
                    response.isError = true;
                    response.message = "Helper already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjSTF02.F02F01))
                {
                    response.isError = true;
                    response.message = "No Helper found";
                }
            }

            return response;
        }

        /// <summary>
        /// Saves the USR01 object
        /// </summary>
        /// <returns>Response indicating the result of the save operation</returns>
        public Response Save()
        {
            Response response = new Response();

            objBLUSR01Handler.ObjOperations = ObjOperations;

            using (var db = _dbFactory.OpenDbConnection())
            {
                using (var transaction = db.OpenTransaction(System.Data.IsolationLevel.Serializable))
                {
                    try
                    {

                        if (ObjOperations == enmOperations.I)
                        {
                            int stf02Id = (int)db.Insert<STF02>(ObjSTF02, selectIdentity: true);
                            
                            objBLUSR01Handler.PreSave(ObjSTF02.F02F02, enmUserRole.H, stf02Id);

                            response = objBLUSR01Handler.Validation();

                            if (!response.isError)
                            {
                                db.InsertOnly<USR01>(objBLUSR01Handler.ObjUSR01,
                                    x => new { x.R01F02, x.R01F03, x.R01F04, x.R01F07 });

                                response.message = "Helper Inserted successfully";
                                transaction.Commit();
                            }
                        }
                        else if (ObjOperations == enmOperations.U)
                        {
                            db.Update(ObjSTF02);
                            response.message = "Helper Updated successfully";
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
        /// Retrieves all STF02 objects
        /// </summary>
        /// <returns>Response containing all STF02 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objDBSTF02Context.Select();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validates object before deleting
        /// </summary>
        /// <param name="id">Id of helper to be validate</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();
            if (!IsExist(id))
            {
                response.isError = true;
                response.message = "Helper Not Found";
            }
            return response;
        }

        /// <summary>
        /// Deletes STF01 object
        /// </summary>
        /// <param name="id">Id of helper to be delete</param>
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

                        db.DeleteById<STF02>(id);

                        transaction.Commit();

                        response.message = "Helper Deleted successfully";
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
        /// Selects data from database and Writes data into file
        /// </summary>
        public void WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                DataTable dataTable = objDBSTF02Context.Select();

                sw.WriteLine("Helper id, Helper name, Role of helper, Working days");

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
            STF02 objSTF02 = objDBSTF02Context.SelectList().FirstOrDefault(h => h.F02F01 == user.R01F07);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objSTF02.F02F02 + DateTime.Now.ToShortDateString());

            DataTable dataTable = objDBSTF02Context.Select();

            DataRow row = dataTable.Rows[objSTF02.F02F01 - 1];

            using (StreamWriter sw = new StreamWriter(newPath))
            {
                sw.WriteLine("Helper id, Helper name, Role of helper, Working days");

                int columnCount = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    if(columnCount == dataTable.Columns.Count - 1)
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
        /// Downloads current logged in helper's file
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public HttpResponseMessage DownloadMyFile(USR01 user)
        {
            WriteMyFile(user);

            STF02 objSTF02 = objDBSTF02Context.SelectList().FirstOrDefault(h => h.F02F01 == user.R01F07);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objSTF02.F02F02 + DateTime.Now.ToShortDateString());

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
                FileName = "Downloaded-Helper-" + Path.GetFileName(newPath)
            };

            return response;
        }

        /// <summary>
        /// Downloads file containing all helper's data
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
                FileName = "Downloaded-Helpers" + Path.GetFileName(path)
            };

            return response;
        }

        #endregion

    }
}