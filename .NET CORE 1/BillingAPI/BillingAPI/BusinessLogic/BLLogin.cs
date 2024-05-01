using BillingAPI.Models.POCO;
using BillingAPI.Repositaries;

namespace BillingAPI.BusinessLogic
{
    /// <summary>
    /// Contains methods needed for login procedure
    /// </summary>
    public class BLLogin : CRUDImplementation<USR01>
    {
        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True if user is valid, false otherwise</returns>
        public USR01? ValidateUser(string username, string password)
        {
            USR01 user = SelectList().FirstOrDefault(u => u.R01F02 == username && u.R01F03 == password);

            return user;
        }

        /// <summary>
        /// Retrives all user's data
        /// </summary>
        /// <returns>List of users</returns>
        public List<USR01> GetUsers()
        {
            List<USR01> lstUSR01 = SelectList();

            return lstUSR01;
        }

    }
}
