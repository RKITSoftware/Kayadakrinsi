using System;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Record table
    /// </summary>
    [Alias("RCD01")]
	public class RCD01
	{
        /// <summary>
        /// Record id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int D01F01 { get; set; }

        /// <summary>
        /// Patient id
        /// </summary>
        [References(typeof(PTN01))]
        [Required]
        public int D01F02 { get; set; }

		/// <summary>
		/// Doctor id
		/// </summary>
		[References(typeof(STF01))]
		[Required]
		public int D01F03 { get; set; }

		/// <summary>
		/// Helper id
		/// </summary>
		[References(typeof(STF02))]
		[Required]
		public int D01F04 { get; set; }

		/// <summary>
		/// Dieases id
		/// </summary>
		[References(typeof(DIS01))]
		[Required]
		public int D01F05 { get; set; }

		/// <summary>
		/// Charge id
		/// </summary>
		[References(typeof(CRG01))]
		[Required]
		public int D01F06 { get; set; }

		/// <summary>
		/// Admit date
		/// </summary>
		[Required]
		public DateTime D01F07 { get; set; } = DateTime.Now;

		/// <summary>
		/// Discharge date
		/// </summary>
		[Required]
		public DateTime D01F08 { get; set; } = DateTime.Now.AddDays(1);

        /// <summary>
        /// Total amount
        /// </summary> 
        [DecimalLength(Precision = 10,Scale = 2)]
        public decimal D01F09 { get; set; }
    }
}