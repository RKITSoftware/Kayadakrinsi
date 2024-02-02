using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAPI.Models;

namespace HospitalAPI.UserRepository
{
    /// <summary>
    /// For validating user
    /// </summary>
    public class UserRepo
    {
        #region Public Members

        /// <summary>
        /// List of users
        /// </summary>
        public static List<USR01> lstUSR01 = new List<USR01>
        {
                new USR01 { R01F01 = 1,R01F02 = "krinsi",R01F03 = "kayada",R01F04="SuperAdmin" },
                new USR01 { R01F01 = 2, R01F02 = "admin", R01F03 = "12345", R01F04 = "Admin" },
                new USR01 { R01F01 = 3, R01F02 = "user", R01F03 = "12345", R01F04 = "User" }
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// vdates user
        /// </summary>
        /// <param name="username">Defines username of user</param>
        /// <param name="password">Defines password of user</param>
        /// <returns>Object of USR01</returns>
        public static USR01 ValidateUser(string username, string password)
        {
            return lstUSR01.FirstOrDefault(user => user.R01F02.Equals(username, StringComparison.OrdinalIgnoreCase) && user.R01F03 == password);
        }

        /// <summary>
        /// For code cleanup
        /// </summary>
        public static void Dispose()
        {
            lstUSR01.Clear();
        }

        #endregion
    }
}