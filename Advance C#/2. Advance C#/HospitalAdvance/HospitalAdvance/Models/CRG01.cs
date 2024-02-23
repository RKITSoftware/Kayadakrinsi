using ServiceStack.DataAnnotations;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// Charges class
	/// </summary>
	[Alias("CRG01")]
	public class CRG01
	{
		/// <summary>
		/// Charge id
		/// </summary>
		[PrimaryKey]
		[AutoIncrement]
        public int G01F01 { get; set; }

		/// <summary>
		/// Doctor id
		/// </summary>
		[References(typeof(STF01))]
		public int G01F02 { get; set; }

		/// <summary>
		/// Dieases id
		/// </summary>
		[References(typeof(DIS01))]
		public int G01F03 { get; set; }

		/// <summary>
		/// Charge amount per treatment
		/// </summary>
		[DecimalLength(Precision =10,Scale = 2)]
		public decimal G01F04 { get; set; }

	}

}