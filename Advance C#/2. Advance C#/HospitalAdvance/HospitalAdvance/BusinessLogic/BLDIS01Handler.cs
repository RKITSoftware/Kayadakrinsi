using System;
using System.Data;
using System.IO;
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
    /// Handles logic for CLDIS01 controller
    /// </summary>
    public class BLDIS01Handler
    {

        #region Private Members

        /// <summary>
        /// Path of file in which dieases data will be written
        /// </summary>
        private readonly string path = HttpContext.Current.Server.MapPath("~/Dieases") +
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
        /// Instance of DIS01 class
        /// </summary>
        public DIS01 ObjDIS01 { get; set; }

        /// <summary>
        /// Instance of DBDIS01Context DB context class
        /// </summary>
        public DBDIS01Context objDBDIS01Context = new DBDIS01Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLDIS01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTODIS01">DTODIS01 object to preprocess</param>
        public void PreSave(DTODIS01 objDTODIS01)
        {
            ObjDIS01 = objDTODIS01.Map<DTODIS01, DIS01>();
        }

        /// <summary>
        /// Checks weather object of DIS01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Create a SQL expression
                var expression = db.From<DIS01>()
                                    .Where(u => u.S01F01 == id);

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
                if (IsExist(ObjDIS01.S01F01))
                {
                    response.isError = true;
                    response.message = "Dieases already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjDIS01.S01F01))
                {
                    response.isError = true;
                    response.message = "No Dieases found";
                }
            }

            return response;
        }

        /// <summary>
        /// Saves the DIS01 object
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
                        db.Insert(ObjDIS01);
                        response.message = "Dieases Inserted successfully";
                    }
                    else
                    {
                        db.Update(ObjDIS01);
                        response.message = "Dieases Updated successfully";
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
        /// Retrieves all DIS01 objects
        /// </summary>
        /// <returns>Response containing all DIS01 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                response.response = objDBDIS01Context.Select();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Selects data from database and Writes data into file
        /// </summary>
        public void WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                DataTable dataTable = objDBDIS01Context.Select();

                sw.WriteLine(string.Format(@"{0,-18} | {1,-18} | {2,-18}", "Dieases id", "Dieases name","Charge Amount"));
                sw.WriteLine(string.Format(@"{0}",new string('-',60)));

                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        sw.Write(string.Format(@"{0,-18} | "
                                                    , row[col]));
                    }
                    sw.WriteLine();
                }

            }
        }

        /// <summary>
        /// Downloads file
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
                FileName = "Downloaded-Dieases-" + Path.GetFileName(path)
            };

            return response;
        }

        #endregion

    }
}
