using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceStack.DataAnnotations;


namespace Test.Models
{
    /// <summary>
    /// Represents movie model for client
    /// </summary>
    public class DTOMOV01
    {
        /// <summary>
        /// Movie Id
        /// </summary>
        [AutoIncrement,PrimaryKey]
        [JsonProperty("V01101")]
        public int V01F01 { get; set; }

        /// <summary>
        /// Movie Name 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required]
        [JsonProperty("V01102")]
        public string V01F02 { get; set; }

        /// <summary>
        /// Duration of movie
        /// </summary>
        [Default(2.5)]
        [System.ComponentModel.DataAnnotations.Range(1,int.MaxValue, ErrorMessage = "Invalid duration of movie")] 
        [JsonProperty("V01103")]
        public double V01F03 { get; set; }

        /// <summary>
        /// Release date of movie
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required]
        [JsonProperty("V01104")]
        public DateTime V01F04 { get; set; }

        /// <summary>
        /// Total collection of movie in crore
        /// </summary>
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue, ErrorMessage = "Invalid amount of collection")]
        [JsonProperty("V01105")]
        public double V01F05 { get; set; }
    }
}