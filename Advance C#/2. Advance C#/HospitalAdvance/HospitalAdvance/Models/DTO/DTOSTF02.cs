using HospitalAdvance.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Helper model for clientside
    /// </summary>
	public class DTOSTF02
    {
        /// <summary>
        /// Helper id
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("F02101")]
        public int F02F01 { get; set; }

        /// <summary>
        /// Helper name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("F02102")]
        public string F02F02 { get; set; }

        /// <summary>
        /// Role of helper 
        /// (Nurse = "NU", Clinical Assistant = "CA", Personal Service Assistant = "PSA", Ward Clerks = "WC"
        /// , Volunteer = "VO")
        /// </summary>
        [Required(ErrorMessage = "Role is required")]
        [JsonProperty("F02103")]
        public enmRole F02F03 { get; set; }

        /// <summary>
		/// Working days of helper
		/// (Sunday = "Su", Monday = "Mo", Tuesday = "Tu", Wednesday = "Wd", Thursday = "Th", Friday = "Fr"
		/// , Saturday = "Sa", All Days = "Al", Week Days = "Wk", Week ends = "We")
		/// </summary>
        [Required(ErrorMessage = "Working days are required")]
        [JsonProperty("F02104")]
        public enmDaysOfWeek F02F04 { get; set; }

    }
}