using BillingAPI.Models;
using BillingAPI.Models.POCO;

namespace BillingAPI.Interfaces
{
    /// <summary>
    /// Defines interface for billing services
    /// </summary>
    public interface IBIL01Service
    {
        /// <summary>
        /// Calculates total amount of bill including taxes
        /// </summary>
        /// <param name="cuurentCMP01">Current company object</param>
        /// <param name="objBIL01">Instance of BIL01 class</param>
        /// <returns></returns>
        public double CalculateTotal(CMP01 cuurentCMP01, BIL01 objBIL01);

        /// <summary>
        /// Generates Bill's pdf
        /// </summary>
        /// <param name="cuurentCMP01">Current company object</param>
        /// <param name="id">Id of bill to be generate</param>
        public Response FinalBill(CMP01 cuurentCMP01, int id);
    }
}
