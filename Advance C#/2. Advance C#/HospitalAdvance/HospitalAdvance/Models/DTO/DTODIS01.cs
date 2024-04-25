using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Dieases  model for clientside
    /// </summary>
    public class DTODIS01
    {
        /// <summary>
        /// Dieases id
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Dieases name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("S01102")]
        public string S01F02 { get; set; }

        /// <summary>
		/// Charge amount
		/// </summary>
        [Required(ErrorMessage = "Charge amount is required")]
        [JsonProperty("S01103")]
        public double S01F03 { get; set; }

    }

}