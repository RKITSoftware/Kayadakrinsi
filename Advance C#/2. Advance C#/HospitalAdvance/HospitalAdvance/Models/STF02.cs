using System;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Defines role of helper
	/// </summary>
	[Flags]
	public enum enmRole
	{
		NURSE = 1,
		CLINICAL_ASSISTANT = 2, 
		PATIENT_SERVICE_ASSISTANT = 3, 
		WARD_CLERKS = 4,
		VOLUNTEER = 5
	}

	/// <summary>
	/// Helper class
	/// </summary>
	[Alias("STF02")]
	public class STF02
	{
		/// <summary>
		/// Helper id
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int F02F01 { get; set; }

		/// <summary>
		/// Helper name
		/// </summary>
		[Required]
        public string F02F02 { get; set; }

		/// <summary>
		/// Role of helper
		/// </summary>
		[Required]
		public enmRole F02F03 { get; set; }

		/// <summary>
		/// Working days of helper
		/// </summary>
		[Required]
		public enmDaysOfWeek F02F04 { get; set; }

		/// <summary>
		/// User id 
		/// </summary>
		[References(typeof(USR01))]
		public int F02F05 { get; set; }

		/// <summary>
		/// Weather user part of hospital or not
		/// </summary>
		[System.ComponentModel.DataAnnotations.Required]
		public bool F02F06 { get; set; } = true;
	}
}