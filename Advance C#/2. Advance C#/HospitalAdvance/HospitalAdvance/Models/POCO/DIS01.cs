using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Dieases class
	/// </summary>
	[Alias("DIS01")]
	public class DIS01
	{
		/// <summary>
		/// Dieases id
		/// </summary>
		[PrimaryKey]
		[AutoIncrement]
		public int S01F01 { get; set; }

		/// <summary>
		/// Dieases name
		/// </summary>
		[Required]
        public string S01F02 { get; set; }

		/// <summary>
		/// Doctor id who cures this dieases
		/// </summary>
		[References(typeof(STF01))]
        public int S01F03 { get; set; }

    }

}