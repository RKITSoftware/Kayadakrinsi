using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Patient class
	/// </summary>
	[Alias("PTN01")]
	public class PTN01
	{
		/// <summary>
		/// Patient id
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int N01F01 { get; set; }

		/// <summary>
		/// Patient name
		/// </summary>
		[Required]
		public string N01F02 { get; set; }

		/// <summary>
		/// Patient mobile number
		/// </summary>
		[Required]
		[DecimalLength(10)]
		public ulong N01F03 { get; set; }

		/// <summary>
		/// Dieases id
		/// </summary>
		[References(typeof(DIS01))]
        public int N01F04 { get; set; }

		/// <summary>
		/// User id 
		/// </summary>
		[References(typeof(USR01))]
		public int N01F05 { get; set; }

		/// <summary>
		/// Weather user part of hospital or not
		/// </summary>
		[System.ComponentModel.DataAnnotations.Required] 
		public bool N01F06 { get; set; } = true;
	}
}