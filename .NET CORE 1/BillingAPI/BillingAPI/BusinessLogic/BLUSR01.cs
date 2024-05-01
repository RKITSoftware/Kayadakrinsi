using System.Text.RegularExpressions;
using BillingAPI.Enums;
using BillingAPI.Models;
using BillingAPI.Models.DTO;
using BillingAPI.Models.POCO;
using BillingAPI.Repositaries;

namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for CLUSR01 controller
    /// </summary>
    public class BLUSR01 : CRUDImplementation<USR01>
    {
        #region Public Members

        /// <summary>
        /// Instance of USR01 class
        /// </summary>
        public USR01 ObjUSR01 { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// PreProcesses object before saving
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 class</param>
        public void PreSave(DTOUSR01 objDTOUSR01)
        {
            ObjUSR01 = objDTOUSR01.Map<DTOUSR01, USR01>();
            obj = ObjUSR01;
        }

        /// <summary>
        /// Validates the object before saving or updating
        /// </summary>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response Validation()
        {
            Response response = new Response();

            string passwordPattern = "^(?=.*[a-z])(?=.*\\d).{8,}$";

            if (!Regex.IsMatch(ObjUSR01.R01F03, passwordPattern))
            {
                response.isError = true;
                response.message = "Password should be of minimun eight characters containing small letters and digits only";
            }
            if (Operations == enmOperations.I)
            {
                if (IsExists(ObjUSR01.R01F01))
                {
                    response.isError = true;
                    response.message = "User already exist";
                }
            }
            else if (Operations == enmOperations.U)
            {
                if (!IsExists(ObjUSR01.R01F01))
                {
                    response.isError = true;
                    response.message = "User not found";
                }
            }

            return response;
        }

        /// <summary>
        /// Validates the object before deleting
        /// </summary>
        /// <param name="id">Id of user to be delete</param>
        /// <returns>Validation response indicating if the object is valid or not</returns>
        public Response ValidationDelete(int id)
        {
            Response response = new Response();

            if (!IsExists(id))
            {
                response.isError = true;
                response.message = "User not found";
            }
            return response;
        }

        #endregion

    }
}
