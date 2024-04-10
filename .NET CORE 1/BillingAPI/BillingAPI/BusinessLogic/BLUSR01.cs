using BillingAPI.Models;
using BillingAPI.Models.DTO;
using BillingAPI.Models.POCO;
using BillingAPI.Repositaries;

namespace BillingAPI.BusinessLogic
{
    public class BLUSR01 : CRUDImplementation<USR01>
    {
        public static USR01 objUSR01;

        public static RES01 RES01;

        public BLMapper<DTOUSR01,USR01> objBLMapper = new BLMapper<DTOUSR01,USR01>();

        public void PreSave(DTOUSR01 objDTOUSR01)
        {
            objUSR01 = objBLMapper.Map(objDTOUSR01);
            obj=objUSR01;
        }

    }
}
