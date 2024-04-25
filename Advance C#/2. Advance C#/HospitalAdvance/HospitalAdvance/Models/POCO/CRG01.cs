using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Charges class
	/// </summary>
	public class CRG01
	{
		/// <summary>
		/// Charge id
		/// </summary>
		[PrimaryKey]
        public int G01F01 { get; set; }

		/// <summary>
		/// Doctor id
		/// </summary>
		public int G01F02 { get; set; }

		/// <summary>
		/// Dieases id
		/// </summary>
		public int G01F03 { get; set; }

		/// <summary>
		/// Charge amount per treatment
		/// </summary>
		public double G01F04 { get; set; }

	}

}