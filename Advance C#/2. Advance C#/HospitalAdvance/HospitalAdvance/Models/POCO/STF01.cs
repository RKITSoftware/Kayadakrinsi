using HospitalAdvance.Enums;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// Doctor class
    /// </summary>
	public class STF01
	{
		/// <summary>
		/// Doctor id 
		/// </summary>
		[PrimaryKey]
		public int F01F01 { get; set; }

		/// <summary>
		/// Doctor name
		/// </summary>
		public string F01F02 { get; set; }

		/// <summary>
		/// Doctor qualification
		/// </summary>
		public string F01F03 { get; set; }

        /// <summary>
        /// Doctor's working days
        /// (Sunday = "Su", Monday = "Mo", Tuesday = "Tu", Wednesday = "Wd", Thursday = "Th", Friday = "Fr"
        /// , Saturday = "Sa", All Days = "Al", Week Days = "Wk", Week ends = "We")
        /// </summary>
        public enmDaysOfWeek F01F04 { get; set; }

	}
}