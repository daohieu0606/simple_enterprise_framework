using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class HandleUser
    {
        public bool AddNewUser(User user)
        {
            return true;
        }
        public bool RemoveUser(User user)
        {
            return true;
        }
        public bool UpdateUser(User user)
        {
            return true;
        }
        public User getUser(string username)
        {
            return new User();
        }

        public User findOneUserByField(string field, string value)
        {
            return new User();
        }
        public User[] findUserByRole(Role role)
        {
            return null;
        }
    }
}
