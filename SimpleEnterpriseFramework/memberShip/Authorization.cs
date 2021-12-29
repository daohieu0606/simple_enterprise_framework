using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Authorization
    {
        public static bool AddRoleToUser(Role role, string id)
        {
            return true;
        }
        public static bool AddRolesToUser(Role[] role,  string id)
        {
            return true;
        }
        public static bool AddRoleToUsers(Role role, string[] id)
        {
            return true;
        }
        public static bool AddRolesToUsers(Role[] role, string[] id)
        {
            return true;
        }
        public static bool RemoveRoleFromUser(Role role,  string id)
        {
            return true;
        }
        public static Role[] getAllRoles()
        {
            return null;
        }
        public static User[] GetUsersInRole(Role role)
        {
            return null;
        }

        public static bool isUserInRole(Role role)
        {
            return true;
        }

        public static Role findRoleByName(string roleName)
        {
            return Role.getInstance(roleName);
        }

        public static Role createRole(Role role)
        {
            //create role in DB
            //...
            Role _role = findRoleByName(role.RoleName);
            return _role;
        }
        public static bool removeRole(string id)
        {
            return true;
        }
        public static bool updateRole( Role newRole)
        {
            return true;
        }


    }
}
