using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FileSystem.Models;

namespace FileSystem.BusinessLogic
{
    /// <summary>
    /// Handle logic for customer controller
    /// </summary>
    public class BLCustomer : ApiController
    {
        /// <summary>
        /// List of customers
        /// </summary>
        public static List<CUS01> lstCustomers = new List<CUS01>
        {
            new CUS01{S01F01=0,S01F02="Wipro",S01F03=12345,S01F04=200},
            new CUS01{S01F01=1,S01F02="Cognizant",S01F03=234,S01F04=500},
            new CUS01{S01F01=2,S01F02="Burbberry",S01F03=234,S01F04=455}
        };

        /// <summary>
        /// Current directory
        /// </summary>
        public static string currentDirectory = Directory.GetCurrentDirectory();

        /// <summary>
        /// Dynamic path to data folder
        /// </summary>
        public static string path = HttpContext.Current.Server.MapPath("~/Data") +"\\"+ DateTime.Now.ToShortDateString() + ".txt";

        /// <summary>
        /// Returns customer with particular id
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>object of class CUS01</returns>
        public CUS01 GetCustomerById(int id)
        {
            var objCUS01 = lstCustomers.FirstOrDefault(c => c.S01F01 == id);
            return objCUS01;
        }

        /// <summary>
        /// Adds new customer to list
        /// </summary>
        /// <param name="objCUS01">object of class CUS01 will be added</param>
        /// <returns>List of customers</returns>
        public List<CUS01> AddCustomer(CUS01 objCUS01)
        {
            var objCUS01Temp = lstCustomers.FirstOrDefault(c => c.S01F01 == objCUS01.S01F01);
            if(objCUS01Temp == null)
            {
                lstCustomers.Add(objCUS01);
            }
            return lstCustomers;
        }

        /// <summary>
        /// Edits customer 
        /// </summary>
        /// <param name="objCUS01">object of class CUS01 will be edited</param>
        /// <returns>List of customers</returns>
        public List<CUS01> UpdateCustomer(CUS01 objCUS01)
        {
            var objCUS01Temp = lstCustomers.FirstOrDefault(c => c.S01F01 == objCUS01.S01F01);
            if (objCUS01Temp != null)
            {
                lstCustomers[objCUS01Temp.S01F01] = objCUS01;
            }
            return lstCustomers;
        }

        /// <summary>
        /// Deletes customer
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>List of customers</returns>
        public List<CUS01> DeleteCustomer(int id)
        {
            lstCustomers.RemoveAt(id); 
            return lstCustomers;
        }

        /// <summary>
        /// Writes customer's details to a file
        /// </summary>
        /// <returns>Appropriate message</returns>
        public string WriteData()
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

        /// <summary>
        /// Downloads file with customer details
        /// </summary>
        /// <returns>File</returns>
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
                FileName = "Downloaded" + Path.GetFileName(path)
            };

            return response;
        }

        public string GetUniqueFileName(string directory, string fileName)
        {
            int count = 1;
            string uniqueFileName = fileName;
            while (File.Exists(Path.Combine(directory, uniqueFileName)))
            {
                uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + count + Path.GetExtension(fileName);
                count++;
            }
            return uniqueFileName;
        }
    }
    
}