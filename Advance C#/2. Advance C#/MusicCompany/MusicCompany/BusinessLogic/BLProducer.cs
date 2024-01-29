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
    public class BLProducer
    {
        public static string path = HttpContext.Current.Server.MapPath("~/App_Data") + "\\" + DateTime.Now.ToShortDateString() + "Producer.txt";

        private readonly static IDbConnectionFactory _dbFactory;
        static BLProducer()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }
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

        public static string WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(Select());
            }
            return "File created successfully 🙌";
        }

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
    }
}