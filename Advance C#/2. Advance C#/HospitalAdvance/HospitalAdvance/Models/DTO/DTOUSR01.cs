using HospitalAdvance.Enums;
using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// User model for clientside
    /// </summary>
	public class DTOUSR01
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required,PrimaryKey]
        [JsonProperty("R01101")]
        public int R01F01 { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required(), Unique]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        [Required]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        [Required]
        [JsonProperty("R01104")]
        public enmUserRole R01F04 { get; set; }

        /// <summary>
        /// Weather user part of hospital or not
        /// </summary>
        [Required]
        [JsonProperty("R01105")]
        public bool R01F05 { get; set; }
    }
}