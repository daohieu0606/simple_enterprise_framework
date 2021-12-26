using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class MemberShip
    {
        public static bool AddNewUser(User user)
        {
            return HandleUser.AddNewUser(user);
        }
        public static bool RemoveUser(string username)
        {
            return HandleUser.RemoveUser(username);
        }
        public static bool UpdateUser(User user)
        {
            return HandleUser.UpdateUser(user);
        }

        public static User findUserById(string id)
        {
            return HandleUser.findUserById(id);
        }
        public static User findOneUserByField(string field, string value)
        {
            return HandleUser.findOneUserByField(field, value);
        }
        public static User[] findUserByRole(Role role)
        {
            return HandleUser.findUserByRole(role);
        }

        public static bool ChangePassword(User user, string newPassword)
        {
            return HandleUser.ChangePassword(user, newPassword);
        }
        public static bool resetPassword(string id, string initPassword)
        {
             return HandleUser.resetPassword(id, initPassword);
        }
        public static bool isExistUser(string username)
        {
            return HandleUser.isExistUser(username);
        }

        public static bool AddRoleToUser(Role role, string id)
        {
            return Authorization.AddRoleToUser(role, id);
        }
        public static bool AddRolesToUser(Role[] role, string id)
        {
            return Authorization.AddRolesToUser(role, id);
        }
        public static bool AddRoleToUsers(Role role, string[] id)
        {
            return Authorization.AddRoleToUsers(role, id);
        }
        public static bool AddRolesToUsers(Role[] role, string[] id)
        {
            return Authorization.AddRolesToUsers(role, id);
        }
        public static bool RemoveRoleFromUser(Role role, string id)
        {
            return Authorization.RemoveRoleFromUser(role, id);
        }
        public static Role[] getAllRole()
        {
            return Authorization.getAllRoles();
        }
        public static User[] GetUsersInRole(Role role)
        {
            return Authorization.GetUsersInRole(role);
        }

        public static bool isUserInRole(Role role)
        {
            return Authorization.isUserInRole(role);
        }

        public static Role createRole(Role role)
        {
            return Authorization.createRole(role);
        }
        public static bool removeRole(string id)
        {
            return Authorization.removeRole(id);
        }
        public static bool updateRole( Role newRole)
        {
            return Authorization.updateRole( newRole);
        }
        public static string Hash(string value)
        {
            return Authentication.Hash(value);
        }

        public static bool validate(string username, string password)
        {
            return Authentication.validate(username, password);
        }
    }
}
