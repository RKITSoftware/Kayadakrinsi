using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Product model for clientside
    /// </summary>
    public class DTOPRO01
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required,Unique]
        [JsonProperty("O01F02")]
        public string O01102 { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [JsonProperty("O01F03")]
        [Default(100)]
        public double O01103 { get; set; } 
    }
}
