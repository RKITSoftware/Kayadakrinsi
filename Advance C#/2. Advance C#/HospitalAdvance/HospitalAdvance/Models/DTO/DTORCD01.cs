using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Record  model for clientside
    /// </summary>
	public class DTORCD01
    {
        /// <summary>
        /// Record id
        /// </summary>
        [Required(ErrorMessage = "Id is required")]
        [JsonProperty("D01101")]
        public int D01F01 { get; set; }

        /// <summary>
        /// Patient id
        /// </summary>
        [Required(ErrorMessage = "Patient Id is required")]
        [JsonProperty("D01102")]
        public int D01F02 { get; set; }

        /// <summary>
        /// Doctor id
        /// </summary>
        [Required(ErrorMessage = "Doctor Id is required")]
        [JsonProperty("D01103")]
        public int D01F03 { get; set; }

        /// <summary>
        /// Helper id
        /// </summary>
        [Required(ErrorMessage = "Helper Id is required")]
        [JsonProperty("D01104")]
        public int D01F04 { get; set; }

        /// <summary>
        /// Admit date
        /// </summary>
        [Required(ErrorMessage = " Admit date is required")]
        [JsonProperty("D01105")]
        public DateTime D01F05 { get; set; }

        /// <summary>
        /// Discharge date
        /// </summary>
        [Required(ErrorMessage = "Discharge date is required")]
        [JsonProperty("D01106")]
        public DateTime D01F06 { get; set; }

    }
}