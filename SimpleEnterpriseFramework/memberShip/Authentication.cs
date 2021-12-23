using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Authentication
    {
       
        public string Hash(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        public bool validate(string username, string password)
        {
            User user = HandleUser.findUserByUsername(username);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

    }
}
