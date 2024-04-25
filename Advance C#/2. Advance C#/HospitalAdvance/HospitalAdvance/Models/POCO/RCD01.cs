using System;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Record table
    /// </summary>
	public class RCD01
	{
        /// <summary>
        /// Record id
        /// </summary>
        [PrimaryKey]
        public int D01F01 { get; set; }

        /// <summary>
        /// Patient id
        /// </summary>
        public int D01F02 { get; set; }

		/// <summary>
		/// Doctor id
		/// </summary>
		public int D01F03 { get; set; }

		/// <summary>
		/// Helper id
		/// </summary>
		public int D01F04 { get; set; }

		/// <summary>
		/// Admit date
		/// </summary>
		public DateTime D01F05 { get; set; } 

		/// <summary>
		/// Discharge date
		/// </summary>
		public DateTime D01F06 { get; set; } 

        /// <summary>
        /// Total amount
        /// </summary> 
        public double D01F07 { get; set; }
    }
}