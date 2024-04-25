using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Patient  model for clientside
    /// </summary>
    public class DTOPTN01
    {
        /// <summary>
        /// Patient id
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("N01101")]
        public int N01F01 { get; set; }

        /// <summary>
        /// Patient name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("N01102")]
        public string N01F02 { get; set; }

        /// <summary>
        /// Patient mobile number
        /// </summary>
        [Required(ErrorMessage = "Contact number is required")]
        [JsonProperty("N01103")]
        public ulong N01F03 { get; set; }

        /// <summary>
        /// Dieases name
        /// </summary>
        [Required(ErrorMessage = "Dieases is required")]
        [JsonProperty("N01104")]
        public int N01F04 { get; set; }

    }
}