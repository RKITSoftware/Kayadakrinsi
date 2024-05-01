using System.ComponentModel.DataAnnotations;
using BillingAPI.Enums;
using Newtonsoft.Json;


namespace BillingAPI.Models.POCO
{
    /// <summary>
    /// Company model for clientside
    /// </summary>
    public class DTOCMP01
    {
        /// <summary>
        /// Id of the company
        /// </summary>
        [Required(ErrorMessage = "Company Id is required")]
        [JsonProperty("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        [Required(ErrorMessage = "Company name is required")]
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// GST number of the company
        /// </summary>
        [Required(ErrorMessage = "GST number is required")]
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Address of the company
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        [JsonProperty("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Name of state in which company is located
        /// </summary>
        [Required(ErrorMessage = "State is required")]
        [JsonProperty("P01105")]
        public enmStateUT P01F05 { get; set; }
    }
}
