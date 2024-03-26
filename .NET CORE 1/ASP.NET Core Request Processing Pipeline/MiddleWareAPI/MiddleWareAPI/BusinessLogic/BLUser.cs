using MiddleWareAPI.Models;

namespace MiddleWareAPI.BusinessLogic
{
    /// <summary>
    /// Contains logic for user
    /// </summary>
    public class BLUser
    {
        #region Public Members

        /// <summary>
        /// Declares count of next Id for auto increment
        /// </summary>
        public static int count = 0;

        /// <summary>
        /// Declares list of users
        /// </summary>
        public static List<USR01> lstUser = new List<USR01>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates appropriate object before adding to list
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public USR01 PreSave(USR01 objUSR01)
        {
            count++;
            objUSR01.
            return objUSR01;
        }

        /// <summary>
        /// Validate object 
        /// </summary>
        /// <param name="objUSR01">Object of class USR01 to be validate</param>
        /// <returns>True if valid object, false otherwise</returns>
        public bool Validation(USR01 objUSR01)
        {
            var user = lstUser.FirstOrDefault(u => u.R01F02 == objUSR01.R01F02);
            if(user != null)
            {
                count--;
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Adds user to lstUSer list
        /// </summary>
        /// <param name="objUSR01">Object of class USR01 to be add</param>
        /// <returns></returns>
        public string AddUser(USR01 objUSR01)
        {
            lstUser.Add(objUSR01);

            return "User added successfully.";
        }

        #endregion

    }
}
