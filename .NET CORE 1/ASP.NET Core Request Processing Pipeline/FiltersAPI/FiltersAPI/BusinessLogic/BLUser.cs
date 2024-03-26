using FiltersAPI.Models;

namespace FiltersAPI.BusinessLogic
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
        public static List<USR01> lstUSR01 = new List<USR01> 
        { 
            new USR01 { R01F01 = ++count, R01F02 = "krinsi", R01F03 = "123", R01F04 = enmUserRole.Admin}
        };

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Validate object 
        /// </summary>
        /// <param name="objUSR01">Object of class USR01 to be validate</param>
        /// <returns>True if valid object, false otherwise</returns>
        public bool Validation(USR01 objUSR01)
        {
            var user = lstUSR01.FirstOrDefault(u => u.R01F02 == objUSR01.R01F02);

            if (user != null)
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
            count++;
            objUSR01.R01F01 = count;
            lstUSR01.Add(objUSR01);

            return "User added successfully.";
        }

        #endregion

    }
}