using BillingAPI.Enums;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Company model for clientside
    /// </summary>
    public class DTOCMP01
    {
        /// <summary>
        /// Name of the company
        /// </summary>
        [Required,Unique]
        [JsonProperty("P01F02")]
        public string P01102 { get; set; }

        /// <summary>
        /// GST number of the company
        /// </summary>
        [ValidateRegularExpression("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")]
        [JsonProperty("P01F03")]
        public string P01103 { get; set; }

        /// <summary>
        /// Address of the company
        /// </summary>
        [JsonProperty("P01F04")]
        [Default("Rajkot")]
        public string P01104 { get; set; }

        /// <summary>
        /// Name of state in which company is located
        /// </summary>
        [JsonProperty("P01F05")]
        [Default(11)]
        public enmStateUT P01105{ get; set; }
    }
}
