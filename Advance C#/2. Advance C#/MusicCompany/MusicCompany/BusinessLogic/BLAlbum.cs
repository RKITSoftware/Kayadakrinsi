using System;
using System.Collections.Generic;
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
    /// Handles logic for album controller
    /// </summary>
    public class BLAlbum
    {
        #region Private Members

        /// <summary>
        /// Path of file in which album data will be written
        /// </summary>
        private readonly static string path = HttpContext.Current.Server.MapPath("~/App_Data") + 
                                              "\\" + DateTime.Now.ToShortDateString() + "Album.txt";

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly static IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        static BLAlbum()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Insert album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class</param>
        /// <returns>Appropriate Message</returns>
        public static string Insert(ALB01 objALB01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ALB01>())
                {
                    db.CreateTable<ALB01>();
                }
                db.Insert(objALB01);
                return "Album added successfully!";
            }
        }

        /// <summary>
        /// Update album
        /// </summary>
        /// <param name="objALB01">object of ALB01 class</param>
        /// <returns>Appropriate Message</returns>
        public static string Update(ALB01 objALB01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ALB01>())
                {
                    db.CreateTable<ALB01>();
                    return "No records to be update!";
                }
                db.Update(objALB01);
                return "Album updated successfully!";
            }
        }

        /// <summary>
        /// Delete album
        /// </summary>
        /// <param name="id">album id to be delete</param>
        /// <returns>Appropriate Message</returns>
        public static string Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ALB01>())
                {
                    db.CreateTable<ALB01>();
                    return "No records to be delete!";
                }
                db.Delete(id);
                return "Album deleted successfully";
            }
        }

        /// <summary>
        /// Select data from ALB01
        /// </summary>
        /// <returns>Serialized string or appropriate message</returns>
        public static List<ALB01> Select()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ALB01>())
                {
                    db.CreateTable<ALB01>();
                }
                return db.Select<ALB01>();
            }
        }

        /// <summary>
        /// Selects data from database and Writes data into file
        /// </summary>
        /// <returns>Appropriate Message</returns>
        public static string WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                var lstALB01 = Select();
                sw.WriteLine("Album id, Album name, No of songs, Producer id, Artist id");
                foreach (var obj in lstALB01)
                {
                    sw.Write(obj.B01F01 + ", ");
                    sw.Write(obj.B01F02 + ", ");
                    sw.Write(obj.B01F03 + ", ");
                    sw.Write(obj.B01F04 + ", ");
                    sw.Write(obj.B01F05);
                    sw.WriteLine();
                }
            }
            return "File created successfully 🙌";
        }

        /// <summary>
        /// Download file
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