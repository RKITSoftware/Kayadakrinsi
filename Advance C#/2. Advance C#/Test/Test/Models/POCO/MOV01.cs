using System;
using ServiceStack.DataAnnotations;

namespace Test.Models
{
    /// <summary>
    /// Represents model for movie
    /// </summary>
    [Alias("MOV01")]
    public class MOV01
    {
        /// <summary>
        /// Movie Id
        /// </summary>
        public int V01F01 { get; set; }

        /// <summary>
        /// Movie Name 
        /// </summary>
        public string V01F02 { get; set; }

        /// <summary>
        /// Duration of movie
        /// </summary>
        public double V01F03{ get; set; }

        /// <summary>
        /// Release date of movie
        /// </summary>
        public DateTime V01F04 { get; set; }

        /// <summary>
        /// Total collection of movie in crore
        /// </summary>
        public double V01F05 { get; set; }

        /// <summary>
        /// Date of record
        /// </summary>
        [Required]
        public DateTime V01F06 { get; set; }
    }
}