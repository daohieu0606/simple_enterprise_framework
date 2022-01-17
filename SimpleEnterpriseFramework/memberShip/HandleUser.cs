using System;
using System.Collections.Generic;
using System.Text;

namespace Membership
{
    public class HandleUser
    {
        public static bool AddNewUser(User user)
        {
            return true;
        }
        public static bool RemoveUser(User user)
        {
            return true;
        }
        public static bool UpdateUser(User user)
        {
            return true;
        }
        public static User getUser(string username)
        {
            return new User();
        }
        public static User findUserByUsername(string username)
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
        public static void resetPassword(User user)
        {

        }
    }
}
