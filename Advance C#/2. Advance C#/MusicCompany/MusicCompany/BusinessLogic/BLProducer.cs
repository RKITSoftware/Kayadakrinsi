using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using MusicCompany.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace MusicCompany.BusinessLogic
{
    /// <summary>
    /// Handles logic for producer controller
    /// </summary>
    public class BLProducer
    {
        #region Private Members

        /// <summary>
        /// Path of file in which album data will be written
        /// </summary>
        private readonly static string path = HttpContext.Current.Server.MapPath("~/App_Data") + "\\" + DateTime.Now.ToShortDateString() + "Producer.txt";

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly static IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        static BLProducer()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts producer
        /// </summary>
        /// <param name="objPRO01">object of class PRO01</param>
        /// <returns>Appropriate Message</returns>
        public static string Insert(PRO01 objPRO01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<PRO01>())
                {
                    db.CreateTable<PRO01>();
                }
                db.Insert(objPRO01);
                return "Producer added successfully!";
            }
        }

        /// <summary>
        /// Updates producer
        /// </summary>
        /// <param name="objPRO01">object of class PRO01</param>
        /// <returns>Appropriate Message</returns>
        public static string Update(PRO01 objPRO01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<PRO01>())
                {
                    db.CreateTable<PRO01>();
                    return "No records to be update!";
                }
                db.Update(objPRO01);
                return "Producer updated successfully!";
            }
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">id of producer to be deleted</param>
        /// <returns>Appropriate Message</returns>
        public static string Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<PRO01>())
                {
                    db.CreateTable<PRO01>();
                    return "No records to be delete!";
                }
                db.Delete(id);
                return "Producer deleted successfully";
            }
        }

        /// <summary>
        /// Selects data of producers
        /// </summary>
        /// <returns>Serialized string or appropriate message</returns>
        public static string Select()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<PRO01>())
                {
                    db.CreateTable<PRO01>();
                    return "No records are founded!";
                }
                return BLSerialize.Serialize(db.Select<PRO01>());
            }
        }

        /// <summary>
        /// Selects data from databse and Writes data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        public static string WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(Select());
            }
            return "File created successfully 🙌";
        }

        /// <summary>
        /// Downloads file
        /// </summary>
        /// <returns>HttpResponseMessage with file</returns>
        public static HttpResponseMessage Download()
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
                FileName = "Downloaded" + Path.GetFileName(path)
            };

            return response;
        }

        #endregion
    }
}