using System.Text.RegularExpressions;
using BillingAPI.Enums;
using BillingAPI.Filters;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;

namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLBIL01 controller
    /// </summary>
    public class BLBIL01
    {

        #region Public Members

        /// <summary>
        /// Instance of ICRUDService interface
        /// </summary>
        public ICRUDService<BIL01> objCRUDBIL01;

        /// <summary>
        /// Instance of IBIL01Service interface
        /// </summary>
        public IBIL01Service objBIL01Service;

        /// <summary>
        /// Istance of BIL01 class
        /// </summary>
        public BIL01 ObjBIL01 { get; set; }

        /// <summary>
        /// Type of operation to be perform
        /// </summary>
        public enmOperations Operations { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Resolves dependecies and performs initialization 
        /// </summary>
        /// <param name="objICRUDServiceBIL01">Instance of ICRUDService<BIL01> interface</param>
        /// <param name="objBIL01Service">Instance of IBIL01Service interface</param>
        public BLBIL01(ICRUDService<BIL01> objICRUDServiceBIL01, IBIL01Service objBIL01Service)
        {
            objCRUDBIL01 = objICRUDServiceBIL01;
            this.objBIL01Service = objBIL01Service;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// PreProcesses object before saving
        /// </summary>
        /// <param name="objDTOBIL01">Instance of DTOBIL01 class</param>
        public void PreSave(DTOBIL01 objDTOBIL01)
        {
            ObjBIL01 = objDTOBIL01.Map<DTOBIL01, BIL01>();
            ObjBIL01.L01F02 = AuthenticationFilter.objUSR01.R01F01;
            ObjBIL01.L01F07 = CalculateTotal();
            objCRUDBIL01.obj = ObjBIL01;
            objCRUDBIL01.Operations = Operations;
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            try
            {
                if (Operations == enmOperations.I)
                {
                    if (objCRUDBIL01.IsExists(ObjBIL01.L01F01))
                    {
                        response.isError = true;
                        response.message = "Bill already exist";
                    }
                }
                else if (Operations == enmOperations.I)
                {
                    if (!objCRUDBIL01.IsExists(ObjBIL01.L01F01))
                    {
                        response.isError = true;
                        response.message = "Bill not found";
                    }
                }

                string pattern = "^[A-Z]{2}[0-9]{2}[A-Z]{2}[0-9]{4}$";

                if (!Regex.Match(ObjBIL01.L01F04, pattern).Success)
                {
                    response.isError = true;
                    response.message = "Invalid trasportation number";
                }
                else if (BLCMP01.currentCMP01.P01F01 == ObjBIL01.L01F03)
                {
                    response.isError = true;
                    response.message = "Buyer and seller are same company";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Validates the object before deleting
        /// </summary>
        /// <param name="id">Id of bill to be delete</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();

            if (!objCRUDBIL01.IsExists(id))
            {
                response.isError = true;
                response.message = "Bill not found";
            }
            return response;
        }

        /// <summary>
        /// Calculates total amount of bill including taxes
        /// </summary>
        /// <returns>Total amount of bill</returns>
        public double CalculateTotal()
        {
            return objBIL01Service.CalculateTotal(BLCMP01.currentCMP01, ObjBIL01);
        }

        /// <summary>
        /// Generates bill's pdf
        /// </summary>
        /// <param name="id">Id of bill to be generate</param>
        public Response FinalBill(int id)
        {
            Response response = objBIL01Service.FinalBill(BLCMP01.currentCMP01, id);

            return response;
        }

        #endregion
    }
}
