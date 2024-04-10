using Newtonsoft.Json;

namespace CRUD_API.Models
{
    /// <summary>
    /// Contains details of orders
    /// </summary>
    public class ORD01
    {
        /// <summary>
        /// Id of order
        /// </summary>
        public int D01F01 { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [JsonProperty("D01102")]
        public string D01F02 { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("D01103")]
        public int D01F03 { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("D01104")]
        public double D01F04 { get; set; }
    }
}