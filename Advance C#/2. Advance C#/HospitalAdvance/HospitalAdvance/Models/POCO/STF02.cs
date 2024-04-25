using HospitalAdvance.Enums;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Helper class
    /// </summary>
	public class STF02
	{
		/// <summary>
		/// Helper id
		/// </summary>
		[PrimaryKey]
		public int F02F01 { get; set; }

		/// <summary>
		/// Helper name
		/// </summary>
        public string F02F02 { get; set; }

        /// <summary>
        /// Role of helper 
		/// (Nurse = "NU", Clinical Assistant = "CA", Personal Service Assistant = "PSA", Ward Clerks = "WC"
		/// , Volunteer = "VO")
        /// </summary>
        public enmRole F02F03 { get; set; }

		/// <summary>
		/// Working days of helper
		/// (Sunday = "Su", Monday = "Mo", Tuesday = "Tu", Wednesday = "Wd", Thursday = "Th", Friday = "Fr"
		/// , Saturday = "Sa", All Days = "Al", Week Days = "Wk", Week ends = "We")
		/// </summary>
		public enmDaysOfWeek F02F04 { get; set; }

	}
}