using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TpesClass.Models
{
    public class ADM01
    {
        #region Public Members

        /// <summary>
        /// Id of admin
        /// </summary>
        public int M01F01 { get; set; }

        /// <summary>
        /// Username of admin
        /// </summary>
        public string M01F02 { get; set; }

        /// <summary>
        /// Password of admin
        /// </summary>
        public string M01F03 { get; set; }

        /// <summary>
        /// Role of admin
        /// </summary>
        public string M01F04 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new admin with id, user name, password with role equals to admin
        /// </summary>
        /// <param name="id">id of admin</param>
        /// <param name="username">user name of admin</param>
        /// <param name="password">password of admin</param>
        public ADM01(int id, string username, string password)
        {
            M01F01 = id;
            M01F02 = username;
            M01F03 = password;
            M01F04 = "admin";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Declares data of admin
        /// </summary>
        /// <returns>List of admins</returns>
        public static List<ADM01> GetData()
        {
            var data = new List<ADM01>
            {
                new ADM01(1, "krinsi", "kayada"),
                new ADM01(2, "rohit", "12345")
            };
            return data;
        }

        #endregion
    }
}