using System.Collections.Generic;


namespace HospitalAPI.Models
{
    /// <summary>
    /// Declares users
    /// </summary>
    public class USR01
    {
        #region Public Members

        /// <summary>
        /// Declares id of user
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Declares user name of user
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Declares password of user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Declares role of user
        /// </summary>
        public string R01F04 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates object of user with id, user name, password, role
        /// </summary>
        /// <param name="id">Defines id of user</param>
        /// <param name="userName">Defines user name of user</param>
        /// <param name="password">Defines password of user</param>
        /// <param name="role">Defines role of user</param>
        public USR01(int id, string userName, string password, string role)
        {
            R01F01 = id;
            R01F02 = userName;
            R01F03 = password;
            R01F04 = role;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates list of users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetUsers()
        {
            var users = new List<USR01>
            {
                new USR01(1,"krinsi","kayada","SuperAdmin"),
                new USR01(2,"admin","12345","Admin"),
                new USR01(3,"user","12345","User")
            };
            return users;
        }

        #endregion
    }
}