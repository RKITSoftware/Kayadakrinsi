using Newtonsoft.Json;

namespace CRUD_API.Models
{
    /// <summary>
    /// Contains details of orders
    /// </summary>
    public class DTOORD01
    {
        
        /// <summary>
        /// Product name
        /// </summary>
        [JsonProperty("D01F02")]
        public string D01102 { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("D01F03")]
        public int D01103 { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("D01F04")]
        public double D01104 { get; set; }
    }
}