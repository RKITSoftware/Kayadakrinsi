using BillingAPI.Enums;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Represents model class for company
    /// </summary>
    public class CMP01
    {
        /// <summary>
        /// Id of the company
        /// </summary>
        [AutoIncrement,PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        [Required,Unique]
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// GST number of the company
        /// </summary>
        [ValidateRegularExpression("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")]
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Address of the company
        /// </summary>
        [JsonProperty("P01104")]
        [Default("Rajkot")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Name of state in which company is located
        /// </summary>
        [JsonProperty("P01105")]
        [Default(11)]
        public enmStateUT P01F05{ get; set; }
    }
}
