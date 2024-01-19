using System;
using System.Collections.Generic;
using System.Linq;
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
        public static List<USR01> users = GetUsers();

        #endregion

        #region Public Methods
        /// <summary>
        /// Creates object of user with id, user name, password, role
        /// </summary>
        /// <param name="id">Defines id of user</param>
        /// <param name="userName">Defines user name of user</param>
        /// <param name="password">Defines password of user</param>
        /// <param name="role">Defines role of user</param>
        public static USR01 NewUser(int id, string userName, string password, string role)
        {
            var objUSR01 = new USR01();
            objUSR01.R01F01 = id;
            objUSR01.R01F02 = userName;
            objUSR01.R01F03 = password;
            objUSR01.R01F04 = role;
            return objUSR01;
        }

        /// <summary>
        /// Creates list of users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetUsers()
        {
            var users = new List<USR01>();
            users.Add(NewUser(1, "krinsi", "kayada", "SuperAdmin"));
            users.Add(NewUser(2, "deep", "patel", "Admin"));
            users.Add(NewUser(3, "extra", "12345", "User"));
            return users;
        }
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