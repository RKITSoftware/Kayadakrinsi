using System.ComponentModel.DataAnnotations;
using HospitalAdvance.Enums;
using Newtonsoft.Json;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Doctor model for clientside
    /// </summary>
	public class DTOSTF01
    {
        /// <summary>
        /// Doctor id 
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("F01101")]
        public int F01F01 { get; set; }

        /// <summary>
        /// Doctor name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("F01102")]
        public string F01F02 { get; set; }

        /// <summary>
        /// Doctor qualification
        /// </summary>
        [Required(ErrorMessage = "Qualification is required")]
        [JsonProperty("F01103")]
        public string F01F03 { get; set; }

        /// <summary>
        /// Doctor's working days
        /// (Sunday = "Su", Monday = "Mo", Tuesday = "Tu", Wednesday = "Wd", Thursday = "Th", Friday = "Fr"
        /// , Saturday = "Sa", All Days = "Al", Week Days = "Wk", Week ends = "We")
        /// </summary>
        [Required(ErrorMessage = "Working days are required")]
        [JsonProperty("F01104")]
        public enmDaysOfWeek F01F04 { get; set; }

    }
}