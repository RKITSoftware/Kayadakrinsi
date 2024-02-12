using System.Collections.Generic;
using TpesClass.Models;

namespace TpesClass.BusinessLogic
{
    /// <summary>
    /// Contains logic for admin controller
    /// </summary>
    public class BLAdmin
    {
        /// <summary>
        /// List of admins
        /// </summary>
        public static List<ADM01> admins = new List<ADM01>
        {
            new ADM01{M01F01 = 1, M01F02 = "krinsi", M01F03 = "kayada", M01F04 = "admin" },
            new ADM01{M01F01 = 2, M01F02 = "rohit", M01F03 = "12345", M01F04 = "admin" }
        };
    }
}