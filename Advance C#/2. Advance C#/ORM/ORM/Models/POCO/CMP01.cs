using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace ORM.Models
{
    /// <summary>
    /// Defines company details
    /// </summary>
    [Alias("CMP01")]
    public class CMP01
    {

        /// <summary>
        /// Declares id of company
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int P01F01 { get; set; }

        /// <summary>
        /// Declares name of company
        /// </summary>
        [Required,Unique]
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Declares city of company
        /// </summary>
        [Required]
        [Default("'Rajkot'")]
        [JsonProperty("P01103")]
        public string P01F03 { get; set; } 

        /// <summary>
        /// Declares type of company
        /// </summary>
        [Required]
        [Default("'IT'")]
        [JsonProperty("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Declares number of employees in a company
        /// </summary>
        [Required]
        [Default(200)]
        [JsonProperty("P01105")]
        public int P01F05 { get; set; }

    }
}