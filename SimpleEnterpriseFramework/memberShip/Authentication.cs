using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Authentication
    {
        private static int salt = 12;
        public static string Hash(string value)
        {
            if(value == null)
            {
                return "";
            }
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }

        public static bool validate(string username, string password)
        {
            User user = HandleUser.findOneUserByField("username",username);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

    }
}
