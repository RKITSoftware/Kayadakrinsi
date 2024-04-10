using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Represents model class for product
    /// </summary>
    public class PRO01
    {
        /// <summary>
        /// Id of the product
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int O01F01 { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Required,Unique]
        [JsonProperty("O01102")]
        public string O01F02 { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [JsonProperty("O01103")]
        [Default(100)]
        public double O01F03 { get; set; } 
    }
}
