using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthorization.Models;

namespace TokenAuthorization.UserRepository
{
    public class UserRepo
    {
        public static List<User> users = User.GetUsers();
        public static User ValidateUser(string username,string password) {
            return users.FirstOrDefault(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
        }
        public static void Dispose()
        {
            users.Clear();
        }
    }
}