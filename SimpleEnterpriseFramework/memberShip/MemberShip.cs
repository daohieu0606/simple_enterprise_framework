using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemberShip
{
    public class MemberShip
    {
        public static async Task<User> AddNewUserAsync(User user)
        {
            return await HandleUser.AddNewUserAsync(user);
        }
        public static async Task<bool> RemoveUserAsync(string username)
        {
            return await HandleUser.RemoveUserAsync(username);
        }
        public static async Task<bool> UpdateUserAsync(User user)
        {
            return await HandleUser.UpdateUserAsync(user);
        }

        public static async Task<User> findUserByIdAsync(string id)
        {
            return await HandleUser.findUserByIdAsync(id);
        }
        public static async Task<User> findOneUserByFieldAsync(string field, string value)
        {
            return await HandleUser.findOneUserByFieldAsync(field, value);
        }
        public static User[] findUserByRoleAsync(Role role)
        {
            return HandleUser.findUserByRole(role);
        }

        public static bool ChangePasswordAsync(User user, string newPassword)
        {
            return HandleUser.ChangePassword(user, newPassword);
        }
        public static async Task<bool> resetPasswordAsync(string id, string initPassword)
        {
             return await HandleUser.resetPasswordAsync(id, initPassword);
        }
        public static async Task<bool> isExistUser(string username)
        {
            return await HandleUser.isExistUserAsync(username);
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
        public static async Task<Role[]> getAllRoleAsync()
        {
            return await Authorization.getAllRolesAsync();
        }
        public static User[] GetUsersInRole(Role role)
        {
            return HandleUser.GetUsersInRole(role);
        }
        public static Role[] GetRolesOfUser(User user)
        {
            return Authorization.GetRolesOfUser(user);
        }


        public static bool isUserInRole(User user,Role role)
        {
            return Authorization.isUserInRole(user, role);
        }
        public static async Task<Role> findRoleByNameAsync(string roleName)
        {
            try
            {
                return await Authorization.findRoleByNameAsync(roleName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Role> findRoleFieldAsync(string field, string value)
        {
            try
            {
                return await Authorization.findRoleFieldAsync(field, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Role> createRoleAsync(Role role)
        {
            return await Authorization.createRole(role);
        }
        public static async Task<bool> removeRoleAsync(string id)
        {
            return await Authorization.removeRoleAsync(id);
        }
        public static async Task<bool> updateRoleAsync( Role newRole)
        {
            return await Authorization.updateRoleAsync( newRole);
        }
        public static string Hash(string value)
        {
            return Authentication.Hash(value);
        }

        public static async Task<bool> validateAsync(string username, string password)
        {
            return await Authentication.validateAsync(username, password);
        }
    }
}
