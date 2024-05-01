using BillingAPI.Models.DTO;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Represents model class for billing
    /// </summary>
    public class BIL01
    {
        /// <summary>
        /// Number of the bill
        /// </summary>
        [PrimaryKey]
        public int L01F01 { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int L01F02 { get; set; }

        /// <summary>
        /// Purchaser company id
        /// </summary>
        public int L01F03 { get; set; }

        /// <summary>
        /// Transport number of vehical 
        /// </summary>
        public string L01F04 { get; set; }

        /// <summary>
        /// List of products associated with bill
        /// </summary>
        public List<DTOPRO02> L01F05 { get; set; }

        /// <summary>
        /// Date of billing
        /// </summary>
        public DateTime L01F06 { get; set; } 

        /// <summary>
        /// Total amount of bill including tax
        /// </summary>
        public double L01F07 { get; set; } 

    }
}
