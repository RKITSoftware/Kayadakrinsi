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
    /// Handles logic for artist controller
    /// </summary>
    public class BLArtist
    {
        #region Private Members

        /// <summary>
        /// Path of file in which album data will be written
        /// </summary>
        public static string path = HttpContext.Current.Server.MapPath("~/App_Data") + "\\" + DateTime.Now.ToShortDateString() + "Artist.txt";

        /// <summary>
        /// Declares Db factory instance
        /// </summary>
        private readonly static IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        static BLArtist()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts artist
        /// </summary>
        /// <param name="objART01">object of class ART01</param>
        /// <returns>Appropriate Message</returns>
        public static string Insert(ART01 objART01)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ART01>())
                {
                    db.CreateTable<ART01>();
                }
                db.Insert(objART01);
                return "Artist added successfully!";
            }
        }

        /// <summary>
        /// Updates artist
        /// </summary>
        /// <param name="objART01">object of class ART01</param>
        /// <returns>Appropriate Message</returns>
        public static string Update(ART01 objART01)
        {
            using(var db = _dbFactory.OpenDbConnection())
            {
                if(!db.TableExists<ART01>())
                {
                    db.CreateTable<ART01>();
                    return "No records to be update!";
                }
                db.Update(objART01);
                return "Artist updated successfully!";
            }
        }

        /// <summary>
        /// Delete artist
        /// </summary>
        /// <param name="id">id orf artist to be deleted</param>
        /// <returns>Appropriate Message</returns>
        public static string Delete(int id)
        {
            using(var db=_dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ART01>())
                {
                    db.CreateTable<ART01>();
                    return "No records to be delete!";
                }
                db.Delete(id);
                return "Artist deleted successfully";
            }
        }

        /// <summary>
        /// Selects data of artist
        /// </summary>
        /// <returns>List of artist</returns>
        public static List<ART01> Select()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (!db.TableExists<ART01>())
                {
                    db.CreateTable<ART01>();
                }
                return db.Select<ART01>();
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
                var lstART01 = Select();
                sw.WriteLine("Artist id, Artist name, User id");
                foreach(var obj in lstART01)
                {
                    sw.Write(obj.T01F01+", ");
                    sw.Write(obj.T01F02+", ");
                    sw.Write(obj.T01F03);
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