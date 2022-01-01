using Core.Database;
using HelperLibrary;
using IoC.DI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MemberShip
{
    class HandleUser
    {
        private static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();
        public static async Task<User> AddNewUserAsync(User user)

        {
            User returnUser = null;
            returnUser = await findOneUserByFieldAsync("username", user.Username);
            if (returnUser != null)
            {
                throw new Exception("User is already exists!");
            }
            User cloneUser = User.copy(user);
            cloneUser.Password = Authentication.Hash(cloneUser.Password);
            cloneUser.Id = StringHelper.GenerateRandomString();
            DataRow dt = cloneUser.parseToDataRow();

            bool isSuccess = db.Insert(User.nameTable, dt);
            Console.WriteLine(isSuccess);
            if (isSuccess)
            {
                returnUser = await findOneUserByFieldAsync("username", user.Username);
            }

            return returnUser;
        }
        public static async Task<bool> RemoveUserAsync(string username)
        {
            User user = await findOneUserByFieldAsync("username", username);

            if (user == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                bool isSuccess = db.Delete(User.nameTable, user.parseToDataRow());
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static async Task<bool> UpdateUserAsync(User user)
        {
            User oldUser = await findOneUserByFieldAsync("user_id", user.Id);

            if (oldUser == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                bool isSuccess = db.Update(User.nameTable, oldUser.parseToDataRow(), user.parseToDataRow());
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static async Task<User> findUserByIdAsync(string id)
        {
            DataRow dt = await db.GetOneRow(User.nameTable, "id", id);
            return User.getInstance(dt);
        }
        public static async Task<User> findOneUserByFieldAsync(string field, string value)
        {
            User user = null;
            DataRow dt = await db.GetOneRow(User.nameTable, field, value);
            if(dt != null)
            {
                user = User.getInstance(dt);
            }
            return user;
        }
        public static User[] findUserByRole(Role role)
        {
            //db.findDataFrom(string tableName1, string[] key1, string tableName2, string[] key2)
            //db.findDataFrom(string tableName1, string key1, string tableName2, string key2, string valueOfKey)
            //if valueOfKey = null, join two table without "WHERE"
            // Return a DataTable

            return null;
        }

        public static bool ChangePassword(User user, string newPassword)
        {
            User cloneUser = User.copy(user);
            cloneUser.Password = Authentication.Hash(newPassword);

            try
            {
                return db.Update(User.nameTable, user.parseToDataRow(), cloneUser.parseToDataRow());
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public static async Task<bool> resetPasswordAsync(string id, string initPassword)
        {
            User user = await findOneUserByFieldAsync("user_id", id);

            if (user == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                return ChangePassword(user, initPassword);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public static async Task<bool> isExistUserAsync(string username)
        {
            User user = await findOneUserByFieldAsync("username", username);

            return user != null;
        }

        public static User[] GetUsersInRole(Role role)
        {
            //db.findDataFrom()--wating for new API
            return null;
        }
    }
}
