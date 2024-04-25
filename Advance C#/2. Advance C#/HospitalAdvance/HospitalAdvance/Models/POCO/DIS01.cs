using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Dieases class
	/// </summary>
	public class DIS01
	{
		/// <summary>
		/// Dieases id
		/// </summary>
		[PrimaryKey]
		public int S01F01 { get; set; }

		/// <summary>
		/// Dieases name
		/// </summary>
        public string S01F02 { get; set; }

		/// <summary>
		/// Charge amount
		/// </summary>
        public double S01F03 { get; set; }
    }

}