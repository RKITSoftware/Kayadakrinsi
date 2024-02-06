using System.Collections.Generic;
using TpesClass.Models;

namespace TpesClass.BusinessLogic
{
    public class BLUser
    {
        /// <summary>
        /// List of users
        /// </summary>
        public static List<USR01> users = new List<USR01> 
        {
            new USR01{R01F01 = 1, R01F02 = "anmol", R01F03 = "12345", R01F04 = "user" },
            new USR01{ R01F01 = 2, R01F02 = "geet", R01F03 = "12345", R01F04 = "user" },
            new USR01{ R01F01 = 3, R01F02 = "raj", R01F03 = "12345", R01F04 = "user" }
        };
    }
}