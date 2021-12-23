using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Authentication
    {
        public bool ChangePassword(User user, string newPassword)
        {
            return true;
        }
        public string Hash(string value)
        {
            string hashValue = "";
            return hashValue;
        }

        public User Login(string username, string password)
        {
            return new User();
        }

        public void resetPassword(User user)
        {

        }
    }
}
