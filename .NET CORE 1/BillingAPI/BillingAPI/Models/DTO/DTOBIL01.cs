using BillingAPI.Models.DTO;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Billing model for clientside
    /// </summary>
    public class DTOBIL01
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        [References(typeof(USR01))]
        [JsonProperty("L01F02")]
        public int L01102 { get; set; }

        /// <summary>
        /// Purchaser company id
        /// </summary>
        [Required]
        [References(typeof(CMP01))]
        [JsonProperty("L01F03")]
        public int L01103 { get; set; }

        /// <summary>
        /// Transport number of vehical 
        /// </summary>
        [ValidateRegularExpression("^(A-Z)(2)(0-9)(1,2)(?: (A-Z))?(?: (A-Z))? (0-9)(4)$")]
        [JsonProperty("L01F04")]
        public string L01104 { get; set; }

        /// <summary>
        /// List of products associated with bill
        /// </summary>
        [Required]
        [JsonProperty("L01F05")]
        public List<DTOPRO02> L01105 { get; set; }

        /// <summary>
        /// Date of billing
        /// </summary>
        [JsonProperty("L01F06")]
        [Default("'2024-03-10'")]
        public DateTime L01106 { get; set; }

        /// <summary>
        /// Total amount of bill including tax
        /// </summary>
        [JsonProperty("L01F07")]
        [Default(1000)]
        public double L01107 { get; set; }

    }
}
