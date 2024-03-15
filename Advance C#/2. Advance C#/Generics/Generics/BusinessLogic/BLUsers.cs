using System.Collections.Generic;
using Generics.GenericItems;
using Generics.Models;

namespace Generics.BusinessLogic
{
    /// <summary>
    /// Defines logic for user controller
    /// </summary>
    public class BLUsers
    {
        /// <summary>
        /// List of users
        /// </summary>
        public static List<object> lstUserData = new List<object>();

        /// <summary>
        /// Add users
        /// </summary>
        /// <returns>List of users</returns>
        public GenericClass<object> Users()
        {
            GenericClass<object> lstUsers = new GenericClass<object>();
            lstUsers.AddItem(new USR01 {R01F01 = 0, R01F02 = "sonu", R01F03 = "sood", R01F04 = "user" });
            lstUsers.AddItem(new USR01 { R01F01 = 1, R01F02 = "rahul", R01F03 = "verma", R01F04 = "user" });
            lstUsers.AddItem(new USR01 { R01F01 = 2, R01F02 = "arjun", R01F03 = "rathi", R01F04 = "user" });
            return lstUsers;
        }

        /// <summary>
        /// Add admins
        /// </summary>
        /// <returns>List of admins</returns>
        public GenericClass<object> Admins()
        {
            GenericClass<object> lstAdmins = new GenericClass<object>();
            lstAdmins.AddItem(new ADM01 { M01F01 = 0, M01F02 = "priyanka", M01F03 = "chopara", M01F04 = "admin" });
            lstAdmins.AddItem(new ADM01 { M01F01 = 1, M01F02 = "rani", M01F03 = "laxmi", M01F04 = "admin" });
            lstAdmins.AddItem(new ADM01 { M01F01 = 2, M01F02 = "kalpana", M01F03 = "chawala", M01F04 = "admin" });
            return lstAdmins;
        }      
        
        /// <summary>
        /// Combines users
        /// </summary>
        /// <returns>List of all users</returns>
        public GenericClass<object> AllUsers()
        {
            GenericClass<object> lstAllUsers = new GenericClass<object>();
            lstAllUsers = Users();
            lstAllUsers.AddItems(Admins());
            return lstAllUsers;
        }
    }
}