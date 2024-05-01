using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Product model for clientside
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        [Required(ErrorMessage = "Product Id is required")]
        [JsonProperty("O01101")]
        public int O01F01 { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Required(ErrorMessage = "Product name is required")]
        [JsonProperty("O01102")]
        public string O01F02 { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [JsonProperty("O01103")]
        public double O01F03 { get; set; }
    }
}
