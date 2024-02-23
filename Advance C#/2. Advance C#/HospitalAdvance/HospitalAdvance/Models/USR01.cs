using System;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Enum for role of users
	/// </summary>
	[Flags]
	public enum enmUserRole
	{
		Admin = 1,
		Doctor = 2,
		Helper = 3, 
		Patient = 4
	}

	/// <summary>
	/// User class
	/// </summary>
	[Alias("USR01")]
	public class USR01
	{
		/// <summary>
		/// User id
		/// </summary>
		[PrimaryKey]
		[AutoIncrement]
		public int R01F01 { get; set; }

		/// <summary>
		/// User name
		/// </summary>
		public string R01F02 { get; set; }

		/// <summary>
		/// Password of user
		/// </summary>
		public string R01F03 { get; set; }

		/// <summary>
		/// Role of user
		/// </summary>
		public enmUserRole R01F04 { get; set; }
	}
}