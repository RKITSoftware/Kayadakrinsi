using System.Collections.Generic;

namespace TpesClass.Models
{
    public class USR01
    {
        #region Public Members

        /// <summary>
        /// Id of user
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// Username of user
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Password of user
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Role of user
        /// </summary>
        public string R01F04 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new user with id, user name, password with role equals to user
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="username">user name of uaer</param>
        /// <param name="password">password of user</param>
        public USR01(int id,string username,string password)
        {
            R01F01 = id;
            R01F02 = username;
            R01F03 = password;
            R01F04 = "user";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Declares data of users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetData()
        {
            var data = new List<USR01> { 
                new USR01(1,"anmol","12345"),
                new USR01(2,"geet","12345"),
                new USR01(3,"raj","12345")
            };
            return data;
        }

        #endregion
    }
}