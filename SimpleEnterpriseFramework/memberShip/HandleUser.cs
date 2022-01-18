namespace Membership
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Core.Database;
    using IoC.DI;
    using HelperLibrary;
    using Membership_v2;

    public class HandleUser
    {
        public static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();
        public static IUser userInstance = new User();
        public static async Task<IUser> AddNewUserAsync(IUser user)
        {
            IUser returnUser = null;
            returnUser = await findOneUserByFieldAsync("id", user.Id);
            if (returnUser != null)
            {
                throw new Exception("User is already exists!");
            }
            IUser cloneUser = user.clone();
            cloneUser.Password = Authentication.Hash(cloneUser.Password);
            cloneUser.Id = StringHelper.GenerateRandomString();
            DataRow dt = cloneUser.parseToDataRow();

            bool isSuccess = db.Insert(userInstance.NameTable, dt);
            
            if (isSuccess)
            {
                returnUser = await findOneUserByFieldAsync("id", user.Id);
            }

            return returnUser;
        }

        public static async Task<bool> RemoveUserAsync(string username)
        {
            IUser user = await findOneUserByFieldAsync("username", username);

            if (user == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                bool isSuccess = db.Delete(userInstance.NameTable, user.parseToDataRow());
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static async Task<bool> UpdateUserAsync(IUser user)
        {
            IUser oldUser = await findOneUserByFieldAsync("user_id", user.Id);

            if (oldUser == null)
            {
                throw new Exception("The user is not found!");
            }
            try
            {
                bool isSuccess = db.Update(userInstance.NameTable, oldUser.parseToDataRow(), user.parseToDataRow());
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static async Task<IUser> findUserByIdAsync(string id)
        {
            string[] props = new string[1];
            props[0] = "user_id";
            string[] values = new string[1];
            values[0] = id;
            DataRow dt = await db.GetOneRow(userInstance.NameTable, props, values);
            return userInstance.getInstance(dt);
        }

        public static async Task<IUser> findOneUserByFieldAsync(string field, string value)
        {
            IUser user = null;

            DataRow dt = await db.GetOneRow(userInstance.NameTable, field, value);
            if (dt != null)
            {
                user = userInstance.getInstance(dt);
            }
            return user;
        }

        public static async Task<IUser[]> findUserByRoleAsync(string rolename)
        {

            IRole role = await Authorization.findRoleByNameAsync(rolename);
            if (role == null) return null;

            string[] props = new string[1];
            string[] values = new string[1];
            props[0] = "role_id";
            values[0] = role.Id;

            DataTable dt = await db.GetTable(Authorization.nameTable, props, values);
            int numberOfRole = dt.Rows.Count;
            IUser[] users = null;

            if (numberOfRole > 0)
            {
                users = new User[numberOfRole];
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    string user_id = dt.Rows[index].Field<string>("user_id");
                    IUser user = await findUserByIdAsync(user_id);
                    users[index] = user;
                }

            }
            return users;
        }

        public static bool ChangePassword(IUser user, string newPassword)
        {
            IUser cloneUser = user.clone();
            cloneUser.Password = Authentication.Hash(newPassword);

            try
            {
                return db.Update(userInstance.NameTable, user.parseToDataRow(), cloneUser.parseToDataRow());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static async Task<bool> resetPasswordAsync(string id, string initPassword)
        {
            IUser user = await findOneUserByFieldAsync("user_id", id);

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
            IUser user = await findOneUserByFieldAsync("username", username);

            return user != null;
        }
    }
}
