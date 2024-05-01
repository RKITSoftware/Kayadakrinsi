using System.Text.RegularExpressions;
using BillingAPI.Enums;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;

namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLCMP01 controller
    /// </summary>
    public class BLCMP01
    {
        #region Public Members

        /// <summary>
        /// Instance of ICRUDService interface
        /// </summary>
        public ICRUDService<CMP01> objCRUDCMP01;

        /// <summary>
        /// Instance of CMP01 class
        /// </summary>
        public static CMP01 ObjCMP01 { get; set; }

        /// <summary>
        /// Defines current company instance of CMP01 class 
        /// </summary>
        public static CMP01 currentCMP01 { get; set; } = new CMP01
        {
            P01F01 = 0,
            P01F02 = "Demo",
            P01F03 = "24AAACC1206D1ZM",
            P01F04 = "Building-A block-A,street no.1",
            P01F05 = Enums.enmStateUT.GJ
        };

        /// <summary>
        /// Type of operation to be perform
        /// </summary>
        public enmOperations Operations { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Resolves dependecies and performs initialization 
        /// </summary>
        /// <param name="objCRUDCMP01">Instance of ICRUDService<CMP01> interface</param>
        public BLCMP01(ICRUDService<CMP01> objCRUDCMP01)
        {
            this.objCRUDCMP01 = objCRUDCMP01;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// PreProcesses object before saving
        /// </summary>
        /// <param name="objDTOCMP01">Instance of DTOCMP01 class</param>
        public void PreSave(DTOCMP01 objDTOCMP01)
        {
            ObjCMP01 = objDTOCMP01.Map<DTOCMP01, CMP01>();
            objCRUDCMP01.obj = ObjCMP01;
            objCRUDCMP01.Operations = Operations;
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            if (Operations == enmOperations.I)
            {
                if (objCRUDCMP01.IsExists(ObjCMP01.P01F01))
                {
                    response.isError = true;
                    response.message = "Company already exist";
                }
            }
            else if (Operations == enmOperations.U)
            {
                if (!objCRUDCMP01.IsExists(ObjCMP01.P01F01))
                {
                    response.isError = true;
                    response.message = "Company not found";
                }
            }

            string patternGST = @"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";

            string gst = objCRUDCMP01.obj.P01F03;

            if (Regex.Match(gst, patternGST).Success)
            {
                int statecode = Convert.ToInt32(gst.Substring(0, 2));

                int state = Convert.ToInt32(objCRUDCMP01.obj.P01F05);

                if (statecode != state)
                {
                    response.isError = true;
                    response.message = "Invalid state";
                }
            }
            else
            {
                response.isError = true;
                response.message = "Invalid GST";
            }

            return response;
        }

        /// <summary>
        /// Validates the object before deleting
        /// </summary>
        /// <param name="id">Id of company to be delete</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();

            if (!objCRUDCMP01.IsExists(id))
            {
                response.isError = true;
                response.message = "Company not found";
            }
            return response;
        }

        /// <summary>
        /// Sets recently inserted and/or updated company to current company
        /// </summary>
        public static void SetCurrentCompany()
        {
            currentCMP01 = ObjCMP01;
        }

        /// <summary>
        /// Sets current company to company whoose Id is given
        /// </summary>
        /// <param name="id">Id of company to be set</param>
        /// <returns>Response contatining result of operation</returns>
        public Response SetCurrentCompany(int id)
        {
            Response response = new Response();

            CMP01 objCMP01 = objCRUDCMP01.SelectList().FirstOrDefault(c => c.P01F01 == id);

            if (objCMP01 != null)
            {
                currentCMP01 = objCMP01;
            }
            else
            {
                response.isError = true;
                response.message = "Company not found with given id";
            }

            return response;
        }

        #endregion

    }
}
