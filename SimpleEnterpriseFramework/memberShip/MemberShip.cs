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
        public static async Task<bool> AddRolesToUserAsync(Role[] role, string id)
        {
            return await Authorization.AddRolesToUserAsync(role, id);
        }
        public static bool AddRoleToUsers(Role role, string[] id)
        {
            return Authorization.AddRoleToUsers(role, id);
        }
        public static async Task<bool> AddRolesToUsersAsync(Role[] role, string[] id)
        {
            return await Authorization.AddRolesToUsersAsync(role, id);
        }
        public static bool RemoveRoleFromUser(string role_id, string user_id)
        {
            return Authorization.RemoveRoleFromUser(role_id, user_id);
        }
        public static async Task<Role[]> getAllRoleAsync()
        {
            return await Authorization.getAllRolesAsync();
        }
        public static User[] GetUsersInRole(Role role)
        {
            return HandleUser.GetUsersInRole(role);
        }
        public static Role[] GetRolesOfUser(string user_id)
        {
            return Authorization.GetRolesOfUser(user_id);
        }


        public static bool isUserInRole(string user_id,string role_id)
        {
            return Authorization.isUserInRole(user_id, role_id);
        }
        public static async Task<Role> findRoleByNameAsync(string roleName)
        {

                return await Authorization.findRoleByNameAsync(roleName);

        }
        public static async Task<Role> findRoleFieldAsync(string field, string value)
        {

                return await Authorization.findRoleFieldAsync(field, value);

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
