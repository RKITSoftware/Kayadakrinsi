using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using FileSystem.Models;

namespace FileSystem.BusinessLogic
{
    public class BLCustomer : ApiController
    {
        public static List<CUS01> lstCustomers = new List<CUS01>();

        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string path = HttpContext.Current.Server.MapPath("~/Data") +"\\"+ DateTime.Now.ToShortDateString() + ".txt";
      
        public static CUS01 NewCustomer(int id,string name,double debit,double credit)
        {
            var objCUS01 = new CUS01();
            objCUS01.S01F01 = id;
            objCUS01.S01F02 = name;
            objCUS01.S01F03 = debit;
            objCUS01.S01F04 = credit;
            return objCUS01;
        }

        public static List<CUS01> GetCustomers()
        {
            lstCustomers.Add(NewCustomer(0, "Wipro", 12345, 200));
            lstCustomers.Add(NewCustomer(1, "Cognizant", 234, 500));
            lstCustomers.Add(NewCustomer(2, "Burbberry", 2345, 455));
            return lstCustomers;
        }

        public static CUS01 GetCustomerById(int id)
        {
            var objCUS01 = lstCustomers.FirstOrDefault(c => c.S01F01 == id);
            return objCUS01;
        }

        public static List<CUS01> AddCustomer(CUS01 objCUS01)
        {
            var objCUS01Temp = lstCustomers.FirstOrDefault(c => c.S01F01 == objCUS01.S01F01);
            if(objCUS01Temp == null)
            {
                lstCustomers.Add(objCUS01);
            }
            return lstCustomers;
        }

        public static List<CUS01> UpdateCustomer(CUS01 objCUS01)
        {
            var objCUS01Temp = lstCustomers.FirstOrDefault(c => c.S01F01 == objCUS01.S01F01);
            if (objCUS01Temp != null)
            {
                lstCustomers[objCUS01Temp.S01F01] = objCUS01;
            }
            return lstCustomers;
        }

        public static List<CUS01> DeleteCustomer(int id)
        {
            lstCustomers.RemoveAt(id); 
            return lstCustomers;
        }

        public static string WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Customer id,Name,Debit,Credit");
                foreach (CUS01 objCUS01 in lstCustomers)
                {
                    sw.WriteLine(objCUS01.S01F01 + "," + objCUS01.S01F02 + "," + objCUS01.S01F03 + "," + objCUS01.S01F04);
                }
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