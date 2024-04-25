using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Patient class
	/// </summary>
	public class PTN01
	{
		/// <summary>
		/// Patient id
		/// </summary>
		[PrimaryKey]
		public int N01F01 { get; set; }

		/// <summary>
		/// Patient name
		/// </summary>
		public string N01F02 { get; set; }

		/// <summary>
		/// Patient mobile number
		/// </summary>
		public ulong N01F03 { get; set; }

		/// <summary>
		/// Dieases id
		/// </summary>
        public int N01F04 { get; set; }

	}
}