using BillingAPI.Models.DTO;
using Newtonsoft.Json;
using ServiceStack;
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
        [AutoIncrement,PrimaryKey]
        public int L01F01 { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [Required]
        [References(typeof(USR01))]
        [JsonProperty("L01102")]
        public int L01F02 { get; set; }

        /// <summary>
        /// Purchaser company id
        /// </summary>
        [Required]
        [References(typeof(CMP01))]
        [JsonProperty("L01103")]
        public int L01F03 { get; set; }

        /// <summary>
        /// Transport number of vehical 
        /// </summary>
        [ValidateRegularExpression("^(A-Z)(2)(0-9)(1,2)(?: (A-Z))?(?: (A-Z))? (0-9)(4)$")]
        [JsonProperty("L01104")]
        public string L01F04 { get; set; }

        /// <summary>
        /// List of products associated with bill
        /// </summary>
        [Required]
        [JsonProperty("L01105")]
        public List<DTOPRO02> L01F05 { get; set; }

        /// <summary>
        /// Date of billing
        /// </summary>
        [JsonProperty("L01106")]
        [Default("'2024-03-10'")]
        public DateTime L01F06 { get; set; } 

        /// <summary>
        /// Total amount of bill including tax
        /// </summary>
        [JsonProperty("L01107")]
        [Default(1000)]
        public double L01F07 { get; set; } 

    }
}
