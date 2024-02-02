using System.Collections.Generic;
using System.Linq;
using HospitalAPI.Models;

namespace HospitalAPI
{
    /// <summary>
    /// For validating user
    /// </summary>
    public class BLValidateUser
    {
        #region Public Members

        /// <summary>
        /// List of users
        /// </summary>
        public static List<USR01> lstUSR01 = new List<USR01>
        {
                new USR01 { R01F01 = 1, R01F02 = "owner", R01F03 = "12345", R01F04 = "SuperAdmin" },
                new USR01 { R01F01 = 2, R01F02 = "doctor", R01F03 = "12345", R01F04 = "Admin" },
                new USR01 { R01F01 = 3, R01F02 = "helper", R01F03 = "12345", R01F04 = "User" }
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// To check weather user is valid or not
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password of user</param>
        /// <returns>User is valid or not</returns>
        public static bool Login(string username, string password)
        {
            return lstUSR01.Any(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        /// <summary>
        /// To get user
        /// </summary>
        /// <param name="username">username of user<</param>
        /// <param name="password">password of user</param>
        /// <returns>User with given username and password if exist</returns>
        public static USR01 GetUserDetails(string username, string password)
        {
            return lstUSR01.FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        #endregion
    }
}