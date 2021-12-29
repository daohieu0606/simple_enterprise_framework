using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Authentication
    {
       
        public static string Hash(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        public static bool validate(string username, string password)
        {
            User user = HandleUser.findOneUserByField("username",username);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

    }
}
