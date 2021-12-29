using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class HandleUser
    {
        public static User AddNewUser(User user)
        {
            return new User();
        }
        public static bool RemoveUser(string username)
        {
            return true;
        }
        public static bool UpdateUser(User user)
        {
            return true;
        }
 
        public static User findUserById(string id)
        {
            return new User();
        }
        public static User findOneUserByField(string field, string value)
        {
            return new User();
        }
        public static User[] findUserByRole(Role role)
        {
            return null;
        }

        public static bool ChangePassword(User user, string newPassword)
        {
            return true;
        }
        public static bool resetPassword(string id, string initPassword)
        {
            return true;
        }
        public static bool isExistUser(string username)
        {
            return true;
        }
    }
}
