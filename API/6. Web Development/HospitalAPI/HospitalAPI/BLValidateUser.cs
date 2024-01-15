using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalAPI.Models;

namespace HospitalAPI
{
    /// <summary>
    /// For validating user
    /// </summary>
    public class BLValidateUser
    {
        #region Public Methods

        /// <summary>
        /// To check weather user is valid or not
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password of user</param>
        /// <returns>User is valid or not</returns>
        public static bool Login(string username, string password)
        {
            return USR01.GetUsers().Any(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        /// <summary>
        /// To get user
        /// </summary>
        /// <param name="username">username of user<</param>
        /// <param name="password">password of user</param>
        /// <returns>User with given username and password if exist</returns>
        public static USR01 GetUserDetails(string username, string password)
        {
            return USR01.GetUsers().FirstOrDefault(user => user.R01F02.Equals(username) && user.R01F03 == password);
        }

        #endregion
    }
}