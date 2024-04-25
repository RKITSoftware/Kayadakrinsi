using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using HospitalAdvance.DataBase;
using HospitalAdvance.Models;
using ServiceStack.Data;

namespace HospitalAdvance.BusinessLogic
{
    public class BLLogin
    {
        #region Private Members

        /// Declares Db factory instance
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Members

        /// <summary>
        /// Instance of BLCommonHandler class
        /// </summary>
        public BLCommonHandler objBLCommonHandler = new BLCommonHandler();

        /// <summary>
        /// Instance of DBUSR01Context DB context class
        /// </summary>
        public DBUSR01Context objDBUSR01Context = new DBUSR01Context();

        #endregion

        #region Constructors

        /// <summary>
        /// Establishes connection and initializes objects
        /// </summary>
        static BLLogin()
        {
            _dbFactory = HttpContext.Current.Application["dbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Seperates username and password from request
        /// </summary>
        /// <param name="Request">Current request</param>
        /// <returns>Username and password</returns>
        public string[] GetUsernamePassword(HttpRequestMessage Request)
        {
            string authToken = Request.Headers.Authorization.Parameter;
            byte[] authBytes = Convert.FromBase64String(authToken);
            authToken = Encoding.UTF8.GetString(authBytes);
            string[] usernamepassword = authToken.Split(':');
            return usernamepassword;
        }

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True if user is valid, false otherwise</returns>
        public USR01 ValidateUser(string username, string password)
        {

            var user = objDBUSR01Context.SelectList().FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);


            return user;

        }

        #endregion
    }
}