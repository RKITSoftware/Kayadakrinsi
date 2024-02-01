using System.Collections.Generic;
using cutomToken.Models;

namespace cutomToken.BusinessLogic
{
    /// <summary>
    /// Handles business logic for users
    /// </summary>
    public class BLUser
    {
        /// <summary>
        /// List of users
        /// </summary>
        public static List<USR01> lstUser = new List<USR01>
        {
            new USR01{R01F01=1,R01F02="user",R01F03="12345",R01F04="user"},
            new USR01{R01F01=2,R01F02="admin",R01F03="12345",R01F04="admin"}
        };
    }
}