using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthorization.Models;

namespace TokenAuthorization.UserRepository
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
        public static List<USR01> users = USR01.GetUsers();

        #endregion

        #region Public Methods

        /// <summary>
        /// vdates user
        /// </summary>
        /// <param name="username">Defines username of user</param>
        /// <param name="password">Defines password of user</param>
        /// <returns>Object of USR01</returns>
        public static USR01 ValidateUser(string username,string password) {
            return users.FirstOrDefault(user => user.R01F02.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
        }

        /// <summary>
        /// For code cleanup
        /// </summary>
        public static void Dispose()
        {
            users.Clear();
        }

        #endregion
    }
}