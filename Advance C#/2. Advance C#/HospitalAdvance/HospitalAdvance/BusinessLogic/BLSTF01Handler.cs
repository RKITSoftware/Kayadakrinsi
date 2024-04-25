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
using ServiceStack.OrmLite.Legacy;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLSTF01 controller
    /// </summary>
    public class BLSTF01Handler
    {
        #region Private Members

        /// <summary>
        /// Path of file in which doctor data will be written
        /// </summary>
        private readonly string path = HttpContext.Current.Server.MapPath("~/Doctor") +
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
        /// Instance of STF01 class
        /// </summary>
        public STF01 ObjSTF01 { get; set; }

        /// <summary>
        /// Instance of BLUSR01Handler class
        /// </summary>
        public BLUSR01Handler objBLUSR01Handler = new BLUSR01Handler();

        /// <summary>
        /// Instance of DBSTF01Context DB context class
        /// </summary>
        public DBSTF01Context objDBSTF01Context = new DBSTF01Context();

        /// <summary>
        /// Instance BLCommonHandler of class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLSTF01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTOSTF01">DTOSTF01 object to preprocess</param>
        public void PreSave(DTOSTF01 objDTOSTF01)
        {
            ObjSTF01 = objDTOSTF01.Map<DTOSTF01, STF01>();
        }

        /// <summary>
        /// Checks weather object of STF01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<STF01>()
                                    .Where(u => u.F01F01 == id);

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
                if (IsExist(ObjSTF01.F01F01))
                {
                    response.isError = true;
                    response.message = "Doctor already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjSTF01.F01F01))
                {
                    response.isError = true;
                    response.message = "No Doctor found";
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

            // Set the operations for the BLUSR01Handler
            objBLUSR01Handler.ObjOperations = ObjOperations;

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction transaction = db.OpenTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            if (ObjOperations == enmOperations.I)
                            {
                                // Insert record into STF01 table
                                int stf01Id = (int)db.Insert<STF01>(ObjSTF01, selectIdentity: true);

                                // Perform pre-save operations for USR01 table using the generated STF01 ID
                                objBLUSR01Handler.PreSave(ObjSTF01.F01F02, enmUserRole.D, stf01Id);

                                // Validate and save the record in USR01 table
                                response = objBLUSR01Handler.Validation();
                                if (!response.isError)
                                {

                                    db.InsertOnly<USR01>(objBLUSR01Handler.ObjUSR01,
                                        x => new { x.R01F02, x.R01F03, x.R01F04, x.R01F06 });

                                    response.message = "Doctor Inserted successfully";
                                    transaction.Commit(); // Commit the transaction if successful
                                }
                            }
                            else if (ObjOperations == enmOperations.U)
                            {
                                // Update the record in STF01 table if it's an update operation
                                db.Update(ObjSTF01);
                                response.message = "Doctor Updated successfully";
                                transaction.Commit(); // Commit the transaction if successful
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback the transaction in case of exception
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any additional exceptions that might occur during transaction setup or database connection
                response.isError = true;
                response.message = "Error occurred during database operation: " + ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Retrieves all STF01 objects
        /// </summary>
        /// <returns>Response containing all STF01 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objDBSTF01Context.Select();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validates object before deleteing
        /// </summary>
        /// <param name="id">Doctor's id to be validate</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();
            if (!IsExist(id))
            {
                response.isError = true;
                response.message = "Doctor Not Found";
            }
            return response;
        }

        /// <summary>
        /// Deletes STF01 object
        /// </summary>
        /// <param name="id">Doctor's id to be delete</param>
        /// <returns></returns>
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

                        db.DeleteById<STF01>(id);

                        transaction.Commit();

                        response.message = "Doctor Deleted successfully";
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
            string data = objBLCommonHandler.ToString(objDBSTF01Context.Select());

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(string.Format(@"{0}"
                                                , data));
            }
        }

        /// <summary>
        /// Selects data from database and Writes data into file of current user
        /// </summary>
        public void WriteMyFile(USR01 user)
        {
            STF01 objSTF01 = objDBSTF01Context.SelectList()
                                        .FirstOrDefault(d => d.F01F01 == user.R01F06);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objSTF01.F01F02 + DateTime.Now.ToShortDateString());

            DataTable dataTable = objDBSTF01Context.Select();

            DataRow row = dataTable.Rows[objSTF01.F01F01 - 1];

            using (StreamWriter sw = new StreamWriter(newPath))
            {
                sw.WriteLine(string.Format(@"{0}, {1}, {2}, {3}"
                                            , "Doctor id", "Doctor name", "Doctor qualification", "Working days"
                                            ));

                foreach (DataColumn col in dataTable.Columns)
                {
                    sw.Write(string.Format(@"{0}, "
                                                , row[col]));
                }
            }
        }

        /// <summary>
        /// Downloads current logged in Doctor's file
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public HttpResponseMessage DownloadMyFile(USR01 user)
        {
            WriteMyFile(user);

            STF01 objSTF01 = objDBSTF01Context.SelectList().FirstOrDefault(d => d.F01F01 == user.R01F06);

            string newPath = path.Replace(DateTime.Now.ToShortDateString(), objSTF01.F01F02 + DateTime.Now.ToShortDateString());

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
                FileName = "Downloaded-Doctor-" + Path.GetFileName(newPath)
            };

            return response;
        }

        /// <summary>
        /// Downloads file containing all Doctor's data
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
                FileName = "Downloaded-Doctors" + Path.GetFileName(path)
            };

            return response;
        }


        #endregion

    }
}
