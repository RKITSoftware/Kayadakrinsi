using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using HospitalAdvance.Enums;
using HospitalAdvance.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace HospitalAdvance.BusinessLogic
{
    /// <summary>
    /// Handles logic for CLCRG01 controller
    /// </summary>
    public class BLCRG01Handler
    {

        #region Private Members

        /// <summary>
        /// Path of file in which charge data will be written
        /// </summary>
        private readonly string path = HttpContext.Current.Server.MapPath("~/Charge") +
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
        /// Instance of CRG01 class
        /// </summary>
        public CRG01 ObjCRG01 { get; set; }

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        /// <summary>
        /// Instance of BLDIS01Handler class
        /// </summary>
        public BLDIS01Handler objBLDIS01Handler = new BLDIS01Handler();

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes db factory instance
        /// </summary>
        public BLCRG01Handler()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Preprocessing before saving
        /// </summary>
        /// <param name="objDTOCRG01">DTOCRG01 object to preprocess</param>
        public void PreSave(DTOCRG01 objDTOCRG01)
        {
            ObjCRG01 = objDTOCRG01.Map<DTOCRG01, CRG01>();
        }

        /// <summary>
        /// Checks weather object of CRG01 is exist or not
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>True is exist, False otherwise</returns>
        public bool IsExist(int id)
        {
            CRG01 charge;

            using (var db = _dbFactory.OpenDbConnection())
            {
                charge = db.SingleById<CRG01>(id);
            }

            return charge == null ? false : true;
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            STF01 doctor;

            using (var db = _dbFactory.OpenDbConnection())
            {

                doctor = db.SingleById<STF01>(ObjCRG01.G01F02);
            }

            if (!objBLDIS01Handler.IsExist(ObjCRG01.G01F03))
            {
                response.isError = true;
                response.message = "Invalid dieases id";

            }
            else if (doctor == null)
            {
                response.isError = true;
                response.message = "Invalid doctor id";

            }
            else if (ObjOperations == enmOperations.I)
            {
                if (IsExist(ObjCRG01.G01F01))
                {
                    response.isError = true;
                    response.message = "Charge already exist";
                }
            }
            else if (ObjOperations == enmOperations.U)
            {
                if (!IsExist(ObjCRG01.G01F01))
                {
                    response.isError = true;
                    response.message = "No charge found";
                }
            }

            return response;
        }

        /// <summary>
        /// Saves the CRG01 object
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
                        db.Insert(ObjCRG01);
                        response.message = "Inserted successfully";
                    }
                    else
                    {
                        db.Update(ObjCRG01);
                        response.message = "Updated successfully";
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
        /// Retrieves all CRG01 objects
        /// </summary>
        /// <returns>Response containing all CRG01 objects</returns>
        public Response Select()
        {
            Response response = new Response();

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CRG01> lstCRG01 = db.Select<CRG01>();

                    if (lstCRG01.Count > 0)
                    {
                        List<object> charges = new List<object>();

                        foreach (CRG01 objCRG01 in lstCRG01)
                        {
                            var doctor = db.SingleById<STF01>(objCRG01.G01F02).F01F02;
                            var dieases = db.SingleById<DIS01>(objCRG01.G01F03).S01F02;

                            charges.Add(new
                            {
                                G01101 = objCRG01.G01F01,
                                G01102 = doctor,
                                G01103 = dieases,
                                G01104 = objCRG01.G01F04
                            });

                        }
                        response.response = objBLCommonHandler.ToDatatable(charges);
                    }
                    else
                    {
                        response.isError = true;
                        response.message = "No data found";
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
        /// Selects data from database and Writes data into file
        /// </summary>
        public void WriteData()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                List<CRG01> lstCRG01;

                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstCRG01 = db.Select<CRG01>();

                    sw.WriteLine(string.Format(@"{0,-18} | {1,-18} | {2,-18} | {3,-18}"
                                                    , "Charge id", "Doctor name", "Dieases name", "Amount"));
                    sw.WriteLine(string.Format("{0}", new string('-', 85)));

                    foreach (var objCRG01 in lstCRG01)
                    {
                        var doctor = db.SingleById<STF01>(objCRG01.G01F02).F01F02;
                        var dieases = db.SingleById<DIS01>(objCRG01.G01F03).S01F02;

                        sw.WriteLine(string.Format(@"{0,-18} | {1,-18} | {2,-18} | {3,-18}"
                                                    , objCRG01.G01F01, doctor, dieases, objCRG01.G01F04));
                    }
                }
            }
        }

        /// <summary>
        /// Download file
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
                FileName = "Downloaded-Charges" + Path.GetFileName(path)
            };

            return response;
        }

        #endregion

    }
}