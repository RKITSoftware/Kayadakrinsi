using Authentication_Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication_Authorization
{
    public class ValidateUser
    {
        public static bool Login(string username, string password)
        {
           return User.GetUsers().Any(user => user.UserName.Equals(username) && user.Password == password);  
        }

        public static User GetUserDetails(string username, string password)
        {
           return User.GetUsers().FirstOrDefault(user => user.UserName.Equals(username) && user.Password == password);
        }
    }
}