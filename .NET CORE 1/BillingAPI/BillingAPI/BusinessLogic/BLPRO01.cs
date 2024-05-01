using BillingAPI.Enums;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.Models.POCO;

namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLPRO01 controller
    /// </summary>
    public class BLPRO01
    {
        #region Public Members

        /// <summary>
        /// Instance of ICRUDService interface
        /// </summary>
        public ICRUDService<PRO01> objCRUDPRO01;

        /// <summary>
        /// Instance of PRO01 class
        /// </summary>
        public PRO01 ObjUSR01 { get; set; }

        /// <summary>
        /// Type of operation to be perform
        /// </summary>
        public enmOperations Operations { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Resolves dependencies and performs initialization
        /// </summary>
        /// <param name="objCRUDPRO01">Instance of ICRUDService<PRO01> interface</param>
        public BLPRO01(ICRUDService<PRO01> objCRUDPRO01)
        {
            this.objCRUDPRO01 = objCRUDPRO01;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// PreProcesses object before saving
        /// </summary>
        /// <param name="objDTOPRO01"></param>
        public void PreSave(DTOPRO01 objDTOPRO01)
        {
            ObjUSR01 = objDTOPRO01.Map<DTOPRO01, PRO01>();
            objCRUDPRO01.obj = ObjUSR01;
            objCRUDPRO01.Operations = Operations;
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
                if (objCRUDPRO01.IsExists(ObjUSR01.O01F01))
                {
                    response.isError = true;
                    response.message = "Product already exist";
                }
            }
            else if (Operations == enmOperations.U)
            {
                if (!objCRUDPRO01.IsExists(ObjUSR01.O01F01))
                {
                    response.isError = true;
                    response.message = "Product not found";
                }
            }

            return response;
        }

        /// <summary>
        /// Validates the object before deleting
        /// </summary>
        /// <param name="id">Id of product to be delete</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();

            if (!objCRUDPRO01.IsExists(id))
            {
                response.isError = true;
                response.message = "Product not found";
            }
            return response;
        }

        #endregion
    }
}
