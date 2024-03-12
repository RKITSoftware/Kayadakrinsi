using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalAdvance.Models
{
	/// <summary>
	/// For taking input model
	/// </summary>
	public class USR02
	{
		/// <summary>
		/// Object of USR01
		/// </summary>
		public USR01 ObjUSR01 { get; set; }

		/// <summary>
		/// Object of either STF01, STF02 or PTN01
		/// </summary>
		public dynamic ObjRole { get; set; }
	}
}