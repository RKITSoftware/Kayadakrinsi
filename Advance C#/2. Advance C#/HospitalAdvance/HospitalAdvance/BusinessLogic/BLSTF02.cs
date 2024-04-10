using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for stf01 controller
    /// </summary>
    public class BLSTF02
    {

        #region Private Members

        /// <summary>
        /// Path of file in which helper data will be written
        /// </summary>
        private static readonly string path = HttpContext.Current.Server.MapPath("~/Helper") +
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
        static BLSTF02()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepares object as our need
        /// </summary>
        /// <param name="objSTF02">Object of class STF02</param>
        /// <param name="objUSR01">Object of class USR01</param>
        /// <returns>Prepared object</returns>
        public STF02 preSave(STF02 objSTF02, USR01 objUSR01)
        {
            objSTF02.F02F05 = objUSR01.R01F01;

            return objSTF02;
        }

        /// <summary>
        /// Validates given object 
        /// </summary>
        /// <param name="objSTF02">Object of class STF02</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validationInsert(STF02 objSTF02)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var user = db.Select<STF02>().FirstOrDefault(x => x.F02F05 == objSTF02.F02F05);

                if (user == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Validates given object 
        /// </summary>
        /// <param name="objSTF02">Object of class STF02</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validationUpdate(STF02 objSTF02)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var user = db.Select<STF02>().FirstOrDefault(x => x.F02F05 == objSTF02.F02F05);

                if (user != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Insert helper
        /// </summary>
        /// <param name="objSTF02">object of STF02 class</param>
        /// <returns>Appropriate Message</returns>
        public string Insert(STF02 objSTF02)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF02>())
                    {
                        db.CreateTable<STF02>();
                    }

                    db.Insert(objSTF02);

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Update helper
        /// </summary>
        /// <param name="objSTF02">object of STF02 class</param>
        /// <returns>Appropriate Message</returns>
        public string Update(STF02 objSTF02)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF02>())
                    {
                        db.CreateTable<STF02>();
                        return "No records to be updated!";
                    }

                    db.Update(objSTF02, u => u.F02F01 == objSTF02.F02F01);

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Disable activation of helper
        /// </summary>
        /// <param name="objSTF02">object of STF02 class</param>
        /// <returns>Appropriate Message</returns>
        public string Delete(STF02 objSTF02)
        {
            try
            {
                objSTF02.F02F06 = false;

                Update(objSTF02);

                return "Success!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Select data from STF02
        /// </summary>
        /// <returns>Serialized string or appropriate message</returns>
        public List<STF02> Select()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF02>())
                    {
                        db.CreateTable<STF02>();
                    }

                    List<STF02> lstSTF02 = db.Select<STF02>();

                    BLUSR01.CacheOperations("Helpers", lstSTF02);

                    return lstSTF02;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                var lstSTF02 = Select();
                sw.WriteLine("Helper id, Helper name, Role of helper, Working days, User id, IsActive");
                foreach (var obj in lstSTF02)
                {
                    sw.Write(obj.F02F01 + ", ");
                    sw.Write(obj.F02F02 + ", ");
                    sw.Write(obj.F02F03 + ", ");
                    sw.Write(obj.F02F04 + ", ");
                    sw.Write(obj.F02F05 + ", ");
                    sw.Write(obj.F02F06);
                    sw.WriteLine();
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
                var obj = Select().FirstOrDefault(x => x.F02F05 == user.R01F01);
                sw.WriteLine("Helper id, Helper name, Role of helper, Working days, User id, IsActive");
                sw.Write(obj.F02F01 + ", ");
                sw.Write(obj.F02F02 + ", ");
                sw.Write(obj.F02F03 + ", ");
                sw.Write(obj.F02F04 + ", ");
                sw.Write(obj.F02F05 + ", ");
                sw.Write(obj.F02F06);
                sw.WriteLine();
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
                FileName = "Downloaded-Helper-" + Path.GetFileName(newPath)
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
                FileName = "Downloaded-Helpers" + Path.GetFileName(path)
            };

            return response;
        }

        #endregion

    }
}