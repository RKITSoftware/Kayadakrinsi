using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Models;
using MySqlX.XDevAPI.Common;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for controller
    /// </summary>
    public class BLDoctor
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

        /// <summary>
        /// Declares object of BLSerialize class
        /// </summary>
        private BLSerialize _objBLSerialize;

        /// <summary>
        /// Declares object of DL class
        /// </summary>
        private DL _objDL;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLDoctor()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
            _objBLSerialize = new BLSerialize();
            _objDL = new DL();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepares object as our need
        /// </summary>
        /// <param name="objSTF01">Object of class STF01</param>
        /// <param name="objUSR01">Object of class USR01</param>
        /// <returns>Prepared object</returns>
        public STF01 preSave(STF01 objSTF01,USR01 objUSR01)
        {
            objSTF01.F01F05 = _objDL.GetNextAutoIncrementNumber();

            return objSTF01;
        }
        
        /// <summary>
        /// Validates given object 
        /// </summary>
        /// <param name="objSTF01">Object of class STF01</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validationInsert(STF01 objSTF01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var user = db.Select<STF01>().FirstOrDefault(x => x.F01F05 == objSTF01.F01F05);

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
        /// <param name="objSTF01">Object of class STF01</param>
        /// <returns>True if object is valid and false otherwise</returns>
        public bool validationUpdate(STF01 objSTF01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var user = db.Select<STF01>().FirstOrDefault(x => x.F01F05 == objSTF01.F01F05);

                if (user != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Insert doctor
        /// </summary>
        /// <param name="objSTF01">object of STF01 class</param>
        /// <returns>Appropriate Message</returns>
        public string Insert(STF01 objSTF01)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF01>())
                    {
                        db.CreateTable<STF01>();
                    }

                    db.Insert(objSTF01);

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            
        }

        /// <summary>
        /// Update doctor
        /// </summary>
        /// <param name="objSTF01">object of STF01 class</param>
        /// <returns>Appropriate Message</returns>
        public string Update(STF01 objSTF01)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF01>())
                    {
                        db.CreateTable<STF01>();
                        return "No records to be updated!";
                    }

                    db.Update(objSTF01, u => u.F01F01 == objSTF01.F01F01);

                    return "Success!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Disable activation of doctor
        /// </summary>
        /// <param name="objSTF01">object of STF01 class</param>
        /// <returns>Appropriate Message</returns>
        public string Delete(STF01 objSTF01)
        {
            try
            {
                objSTF01.F01F06 = false;

                Update(objSTF01);

                return "Success!";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Select data from STF01
        /// </summary>
        /// <returns>Serialized string or appropriate message</returns>
        public List<STF01> Select()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (!db.TableExists<STF01>())
                    {
                        db.CreateTable<STF01>();
                    }

                    List<STF01> lstSTF01 = db.Select<STF01>();

                    BLUser.CacheOperations("Doctors", lstSTF01);

                    return lstSTF01;
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
                var lstSTF01 = Select();
                var data = _objBLSerialize.Serialize(lstSTF01);
                sw.WriteLine("Doctor id, Doctor name, Doctor qualification, Working days, User id of doctor, IsActive");
                sw.WriteLine(data);
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
                var obj = Select().FirstOrDefault(x => x.F01F05 == user.R01F01);
                sw.WriteLine("Doctor id, Doctor name, Doctor qualification, Working days, User id of doctor, IsActive");
                sw.Write(obj.F01F01 + ", ");
                sw.Write(obj.F01F02 + ", ");
                sw.Write(obj.F01F03 + ", ");
                sw.Write(obj.F01F04.ToString() + ", ");
                sw.Write(obj.F01F05 + ", ");
                sw.Write(obj.F01F06);
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
                FileName = "Downloaded-Doctor-" + Path.GetFileName(newPath)
            };

            return response;
        }

        /// <summary>
        /// Downloads file
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
                FileName = "Downloaded-Doctors" + Path.GetFileName(path)
            };

            return response;
        }


        #endregion
    }
}
