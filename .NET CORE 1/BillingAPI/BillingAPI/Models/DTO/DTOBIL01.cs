using System.ComponentModel.DataAnnotations;
using BillingAPI.Models.DTO;
using Newtonsoft.Json;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Billing model for clientside
    /// </summary>
    public class DTOBIL01
    {
        /// <summary>
        /// Number of the bill
        /// </summary>
        [Required(ErrorMessage = "Bill number is required")]
        [JsonProperty("L01101")]
        public int L01F01 { get; set; }

        /// <summary>
        /// Purchaser company id
        /// </summary>
        /// Purchaser company id
        [Required(ErrorMessage = "Purchaser Company Id is required")]
        [JsonProperty("L01102")]
        public int L01F03 { get; set; }

        /// <summary>
        /// Transport number of vehical 
        /// </summary>
        [Required(ErrorMessage = "Transportation vehical number is required")]
        [JsonProperty("L01103")]
        public string L01F04 { get; set; }

        /// <summary>
        /// List of products associated with bill
        /// </summary>
        [Required(ErrorMessage = "Product's list is required")]
        [JsonProperty("L01104")]
        public List<DTOPRO02> L01F05 { get; set; }

        /// <summary>
        /// Date of billing
        /// </summary>
        [Required(ErrorMessage = "Date is required")]
        [JsonProperty("L01105")]
        public DateTime L01F06 { get; set; }

    }
}
