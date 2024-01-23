using System.Collections.Generic;
using System.Linq;
using Authentication_Authorization.Models;

namespace Authentication_Authorization
{
    /// <summary>
    /// For validating user
    /// </summary>
    public class BLValidateUser
    {
        #region Public Methods
        /// <summary>
        /// Creates list of users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetUsers()
        {
            var users = new List<USR01>
            {
                new USR01 { R01F01 = 1,R01F02="krinsi",R01F03="kayada",R01F04="SuperAdmin" },
                new USR01 { R01F01 = 2, R01F02 = "deep", R01F03 = "patel", R01F04 = "Admin" },
                new USR01 { R01F01 = 3, R01F02 = "extra", R01F03 = "12345", R01F04 = "User" }
            };
            return users;
        }
        /// <summary>
        /// To check weather user is valid or not
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password of user</param>
        /// <returns>User is valid or not</returns>
        public static bool Login(string username, string password)
        {
           return GetUsers().Any(user => user.R01F02.Equals(username) && user.R01F03 == password);  
        }

        /// <summary>
        /// To get user
        /// </summary>
        /// <param name="username">username of user<</param>
        /// <param name="password">password of user</param>
        /// <returns>User with given username and password if exist</returns>
        public static USR01 GetUserDetails(string username, string password)
        {
           return GetUsers().FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        #endregion
    }
}