using Core.Database;
using IoC.DI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MemberShip
{
    class HandleUser
    {
        private static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();
        public static User AddNewUser(User user)
        {
            DataRow dt = user.parseToDataRow();
            User returnUser = null;
            bool isSuccess = db.Insert(User.nameTable, dt);

            if (isSuccess)
            {
                returnUser = findOneUserByField("username", user.Username);
            }

            return  returnUser;
        }
        public static bool RemoveUser(string username)
        {
            User user = findOneUserByField("username", username);

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
        public static bool UpdateUser(User user)
        {
            User oldUser = findOneUserByField("user_id", user.Id);

            if (oldUser == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                bool isSuccess = db.Update(User.nameTable,oldUser.parseToDataRow(), user.parseToDataRow());
                return isSuccess;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
 
        public static User findUserById(string id)
        {
            DataRow dt = db.GetOneRow(User.nameTable, "id", id);
            return User.getInstance(dt);
        }
        public static User findOneUserByField(string field, string value)
        {
            DataRow dt = db.GetOneRow(User.nameTable, field, value);
            return User.getInstance(dt);
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
        public static bool resetPassword(string id, string initPassword)
        {
            User user = findOneUserByField("user_id", id);

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
        public static bool isExistUser(string username)
        {
            User user = findOneUserByField("username", username);

            return user != null;
        }

        public static User[] GetUsersInRole(Role role)
        {
            //db.findDataFrom()--wating for new API
            return null;
        }
    }
}
