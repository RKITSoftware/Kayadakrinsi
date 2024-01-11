using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication_Authorization.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public User(int id,string userName,string password,string role) { 
            Id = id;
            UserName = userName;
            Password = password;
            Roles = role;
        }

        public static List<User> GetUsers()
        {
            var users = new List<User>
            {
                new User(1,"krinsi","kayada","SuperAdmin"),
                new User(2,"deep","patel","Admin"),
                new User(3,"extra","12345","User")
            };
            return users;
        }
    }
}