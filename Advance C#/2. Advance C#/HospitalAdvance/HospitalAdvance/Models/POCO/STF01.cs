using System.ComponentModel.DataAnnotations;
using HospitalAdvance.Enums;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Doctor class
    /// </summary>
    [Alias("STF01")]
	public class STF01
	{
		/// <summary>
		/// User id 
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int F01F01 { get; set; }

		/// <summary>
		/// Doctor name
		/// </summary>
		[System.ComponentModel.DataAnnotations.Required]
		public string F01F02 { get; set; }

		/// <summary>
		/// Doctor qualification
		/// </summary>
		[System.ComponentModel.DataAnnotations.Required]
		public string F01F03 { get; set; }

		/// <summary>
		/// Doctor's working days
		/// </summary>
		[EnumDataType(typeof(enmDaysOfWeek))]
		public enmDaysOfWeek F01F04 { get; set; }

        /// <summary>
        /// User id 
        /// </summary>
        [References(typeof(USR01))]
		public int F01F05 { get; set; }

		/// <summary>
		/// Weather user part of hospital or not
		/// </summary>
		[System.ComponentModel.DataAnnotations.Required]
		public bool F01F06 { get; set; }


	}
}