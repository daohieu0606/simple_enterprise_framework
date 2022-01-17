namespace Membership
{
    using Core.Database;
    using HelperLibrary;
    using IoC.DI;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public class Authorization
    {
        public static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();

        public static string nameTable = "user_role";

        public static async Task<bool> AddRoleToUserAsync(Role role, string id)
        {
            bool isSuccess = true;

            try
            {
                bool userInRole = await isUserInRoleAsync(id, role.Id);

                if (!userInRole)
                {
                    isSuccess = db.Insert(nameTable, User.toUserRoleDataRow(id, role.Id));
                    if (!isSuccess)
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }

        public static async Task<bool> AddRolesToUserAsync(Role[] roles, string id)
        {
            bool isSuccess = true;

            foreach (Role role in roles)
            {
                try
                {
                    isSuccess = await AddRoleToUserAsync(role, id);

                    if (!isSuccess)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return isSuccess;
        }

        public static async Task<bool> AddRoleToUsersAsync(Role role, string[] ids)
        {
            bool isSuccess = true;

            foreach (string id in ids)
            {
                try
                {
                    isSuccess = await AddRoleToUserAsync(role, id);

                    if (!isSuccess)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return isSuccess;
        }

        public static async Task<bool> AddRolesToUsersAsync(Role[] roles, string[] ids)
        {
            bool isSuccess = true;

            foreach (Role role in roles)
            {
                foreach (string id in ids)
                {
                    try
                    {
                        isSuccess = await AddRoleToUserAsync(role, id);

                        if (!isSuccess)
                        {
                            return false;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return isSuccess;
        }

        public static bool RemoveRoleFromUser(string role_id, string user_id)
        {
            try
            {
                return db.Delete(nameTable, User.toUserRoleDataRow(user_id, role_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Role[]> getAllRolesAsync()
        {
            DataTable dt = await db.GetTable(Role.nameTable);
            int numberOfRole = dt.Rows.Count;
            Role[] roles = null;

            if (numberOfRole > 0)
            {
                roles = new Role[numberOfRole];
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    roles[index] = Role.getInstance(dt.Rows[index]);
                }
            }

            return roles;
        }

        public static async Task<Role[]> GetRolesOfUserAsync(string user_id)
        {
            string[] props = new string[1];
            string[] values = new string[1];
            props[0] = "user_id";
            values[0] = user_id;
            DataTable dt = null;
            try
            {
                dt = await db.GetTable(Role.nameTable, props, values);
                if (dt == null)
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            int numberOfRole = dt.Rows.Count;
            Role[] roles = new Role[numberOfRole];

            for (int i = 0; i < numberOfRole; i++)
            {
                DataRow dr = dt.Rows[i];
                Role role = Role.getInstance(dr);
                roles[i] = role;
            }
            return roles;
        }

        public static async Task<bool> isUserInRoleAsync(string user_id, string role_id)
        {
            try
            {
                string[] keys = new string[2];
                string[] values = new string[2];
                keys[0] = "user_id";
                keys[1] = "role_id";
                values[0] = user_id;
                values[1] = role_id;
                DataRow dr = await db.GetOneRow(nameTable, keys, values);
                return dr != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Role> findRoleByNameAsync(string roleName)
        {

            try
            {
                string[] props = new string[1];
                props[0] = "rolename";
                string[] values = new string[1];
                values[0] = roleName;
                DataRow dt = await db.GetOneRow(Role.nameTable, props, values);

                if (dt != null)
                {
                    return Role.getInstance(dt);

                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Role> findRoleFieldAsync(string field, string value)
        {

            try
            {
                string[] props = new string[1];
                props[0] = field;
                string[] values = new string[1];
                values[0] = value;
                DataRow dt = await db.GetOneRow(Role.nameTable, props, values);

                if (dt == null)
                {
                    return null;
                }

                return Role.getInstance(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Role> createRole(Role role)
        {
            try
            {
                Role cloneRole = Role.getInstance(role);
                cloneRole.Id = StringHelper.GenerateRandomString();
                bool isSuccess = db.Insert(Role.nameTable, cloneRole.toDataRow());
                if (!isSuccess)
                {
                    return null;
                }

                return cloneRole;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public static async Task<bool> removeRoleAsync(string id)
        {
            try
            {
                Role role = await findRoleFieldAsync("role_id", id);
                if (role == null)
                {
                    throw new Exception("Role is not found!");
                }

                return db.Delete(Role.nameTable, role.toDataRow());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<bool> updateRoleAsync(Role newRole)
        {
            try
            {
                Role role = await findRoleFieldAsync("role_id", newRole.Id);

                if (role == null)
                {
                    throw new Exception("Role is not found!");
                }

                return db.Update(Role.nameTable, role.toDataRow(), newRole.toDataRow());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
