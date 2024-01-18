using System;
using System.Collections.Generic;
using Generics.GenericItems;
using Generics.Models;

namespace Generics.BusinessLogic
{
    public class BLUsers
    {
        public static List<object> lstUserData = new List<object>();

        public static USR01 NewUsr01(int id,string username,string password)
        {
            var objUSR01 = new USR01();
            objUSR01.R01F01 = id;
            objUSR01.R01F02 = username;
            objUSR01.R01F03 = password;
            objUSR01.R01F04 = "user";
            return objUSR01;
        }

        public static ADM01 NewAdmin(int id, string username, string password)
        {
            var objADM01 = new ADM01();
            objADM01.M01F01 = id;
            objADM01.M01F02 = username;
            objADM01.M01F03 = password;
            objADM01.M01F04 = "admin";
            return objADM01;
        }

        public static GenericClass<object> Users()
        {
            GenericClass<object> lstUsers = new GenericClass<object>();
            lstUsers.AddItem(NewUsr01(0, "sonu", "sood"));
            lstUsers.AddItem(NewUsr01(1, "rahul", "verma"));
            lstUsers.AddItem(NewUsr01(2, "arjun", "rathi"));
            return lstUsers;
        }

        public static GenericClass<object> Admins()
        {
            GenericClass<object> lstAdmins = new GenericClass<object>();
            lstAdmins.AddItem(NewAdmin(0, "priyanka", "chopara"));
            lstAdmins.AddItem(NewAdmin(1, "rani", "laxmi"));
            lstAdmins.AddItem(NewAdmin(2, "kalpana", "chawala"));
            return lstAdmins;
        }      

        public static GenericClass<object> AllUsers()
        {
            GenericClass<object> lstAllUsers = new GenericClass<object>();
            lstAllUsers = Users();
            lstAllUsers.AddItems(Admins());
            return lstAllUsers;
        }
    }
}