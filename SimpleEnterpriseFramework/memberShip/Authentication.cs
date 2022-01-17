using System;
using System.Collections.Generic;
using System.Text;

namespace Membership
{
    public class Authenticaiton
    {
        public string Hash(string value)
        {
            return "string";
        }

        public bool validate(string username, string password)
        {
            User user = HandleUser.findUserByUsername(username);
            return true;
        }
        public bool validateExistenceUser(string username, string password)
        {
            return true;
        }

        public bool createUser(string username, string password)
        {
            return true;
        }
    }
}
