using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// To check weather user is valid or not
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password of user</param>
        /// <returns>User is valid or not</returns>
        public static bool Login(string username, string password)
        {
            return lstUser.Any(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        /// <summary>
        /// To get user
        /// </summary>
        /// <param name="username">username of user<</param>
        /// <param name="password">password of user</param>
        /// <returns>User with given username and password if exist</returns>
        public static USR01 GetUserDetails(string username, string password)
        {
            return lstUser.FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }
    }
}