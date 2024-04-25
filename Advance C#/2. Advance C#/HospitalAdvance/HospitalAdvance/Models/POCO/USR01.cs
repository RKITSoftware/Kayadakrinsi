using HospitalAdvance.Enums;
using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
    /// <summary>
    /// User class
    /// </summary>
	public class USR01
	{
		/// <summary>
		/// User id
		/// </summary>
		[PrimaryKey]
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
		/// Role of user (Manager = "M", Doctor = "D", Helper = "H", Patient = "P")
		/// </summary>
		public enmUserRole R01F04 { get; set; }

        /// <summary>
        /// Is user active ( Yes = "Y", No = "N")
        /// </summary>
        public enmIsActive R01F05 { get; set; }

		/// <summary>
		/// Doctor's Id
		/// </summary>
        public int? R01F06 { get; set; }

        /// <summary>
        /// Helper's Id
        /// </summary>
        public int? R01F07 { get; set; }

        /// <summary>
        /// Patient's Id
        /// </summary>
        public int? R01F08 { get; set; }

    }
}