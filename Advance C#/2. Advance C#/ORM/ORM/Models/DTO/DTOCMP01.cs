using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    public class DTOCMP01
    {

        /// <summary>
        /// Declares name of company
        /// </summary>
        [Required, Unique]
        [JsonProperty("P01F02")]
        public string P01102 { get; set; }

        /// <summary>
        /// Declares city of company
        /// </summary>
        [Required]
        [Default("'Rajkot'")]
        [JsonProperty("P01F03")]
        public string P01103 { get; set; } 

        /// <summary>
        /// Declares type of company
        /// </summary>
        [Required]
        [Default("'IT'")]
        [JsonProperty("P01F04")]
        public string P01104 { get; set; } 

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        [Required]
        [Default(200)]
        [JsonProperty("P01F05")]
        public int P01105 { get; set; } 

    }
}