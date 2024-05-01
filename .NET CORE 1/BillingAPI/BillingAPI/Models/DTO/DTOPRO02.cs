using BillingAPI.Models.POCO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Product id is required")]
        [JsonProperty("O02201")]
        public int O02F01 { get; set; }

        /// <summary>
        /// Product quantity
        /// </summary>
        [Required(ErrorMessage = "Quantity is required")]
        [JsonProperty("O02202")]
        public int O02F03 { get; set; }
    }
}
