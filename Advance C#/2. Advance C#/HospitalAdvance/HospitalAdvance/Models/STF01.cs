using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Enum for working days of doctor
	/// </summary>
	[Flags]
	public enum enmDaysOfWeek
	{
		Sunday = 1,
		Monday = 2,
		Tuesday = 4,
		Wednesday = 8,
		Thursday = 16,
		Friday = 32,
		Saturday = 64,

		All = Weekdays | Weekend,
		Weekdays = Monday | Tuesday | Wednesday | Thursday | Friday,
		Weekend = Sunday | Saturday
	}

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
		/// Doctor;s working days
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