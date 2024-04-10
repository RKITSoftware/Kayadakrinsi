using BillingAPI.Models.POCO;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.DTO
{
    /// <summary>
    /// Represents model for purchased product details
    /// </summary>
    public class DTOPRO02
    {
        /// <summary>
        /// Product id
        /// </summary>
        [References(typeof(PRO01))]
        public int O02201 { get; set; }

        /// <summary>
        /// Product quantity
        /// </summary>
        [Default(1)]
        public int O02202 { get; set; }
    }
}
