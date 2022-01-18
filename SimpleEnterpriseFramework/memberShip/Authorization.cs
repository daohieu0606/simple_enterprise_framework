namespace Membership
{
    using Core.Database;
    using HelperLibrary;
    using IoC.DI;
    using Membership_v2;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public class Authorization
    {
        public static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();
        public static IUser userInstance = new User();
        public static string nameTable = "user_role";
        public static IRole roleInstance = new Role();

        public static async Task<bool> AddRoleToUserAsync(IRole role, string id)
        {
            bool isSuccess = true;

            try
            {
                bool userInRole = await isUserInRoleAsync(id, role.Id);
                
                if (!userInRole)
                {
                    isSuccess = db.Insert(nameTable, userInstance.toUserRoleDataRow(id, role.Id));
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

        public static async Task<bool> AddRolesToUserAsync(IRole[] roles, string id)
        {
            bool isSuccess = true;

            foreach (IRole role in roles)
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

        public static async Task<bool> AddRoleToUsersAsync(IRole role, string[] ids)
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

        public static async Task<bool> AddRolesToUsersAsync(IRole[] roles, string[] ids)
        {
            bool isSuccess = true;

            foreach (IRole role in roles)
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
                return db.Delete(nameTable, userInstance.toUserRoleDataRow(user_id, role_id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<IRole[]> getAllRolesAsync()
        {
            DataTable dt = await db.GetTable(roleInstance.NameTable);
            int numberOfRole = dt.Rows.Count;
            IRole[] roles = null;

            if (numberOfRole > 0)
            {
                roles = new IRole[numberOfRole];
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    roles[index] = roleInstance.getInstance(dt.Rows[index]);
                }
            }

            return roles;
        }

        public static async Task<IRole[]> GetRolesOfUserAsync(string user_id)
        {
            string[] props = new string[1];
            string[] values = new string[1];
            props[0] = "user_id";
            values[0] = user_id;
            DataTable dt = null;
            try
            {
                dt = await db.GetTable(roleInstance.NameTable, props, values);
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
            IRole[] roles = new IRole[numberOfRole];

            for (int i = 0; i < numberOfRole; i++)
            {
                DataRow dr = dt.Rows[i];
                IRole role = roleInstance.getInstance(dr);
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

        public static async Task<IRole> findRoleByNameAsync(string roleName)
        {

            try
            {
                string[] props = new string[1];
                props[0] = "rolename";
                string[] values = new string[1];
                values[0] = roleName;
                DataRow dt = await db.GetOneRow(roleInstance.NameTable, props, values);

                if (dt != null)
                {
                    return roleInstance.getInstance(dt);

                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<IRole> findRoleFieldAsync(string field, string value)
        {

            try
            {
                string[] props = new string[1];
                props[0] = field;
                string[] values = new string[1];
                values[0] = value;
                DataRow dt = await db.GetOneRow(roleInstance.NameTable, props, values);

                if (dt == null)
                {
                    return null;
                }

                return roleInstance.getInstance(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<IRole> createRole(IRole role)
        {
            try
            {
                IRole cloneRole = role.clone();
                cloneRole.Id = StringHelper.GenerateRandomString();
                bool isSuccess = db.Insert(roleInstance.NameTable, cloneRole.toDataRow());
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
                IRole role = await findRoleFieldAsync("role_id", id);
                if (role == null)
                {
                    throw new Exception("Role is not found!");
                }

                return db.Delete(roleInstance.NameTable, role.toDataRow());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<bool> updateRoleAsync(IRole newRole)
        {
            try
            {
                IRole role = await findRoleFieldAsync("role_id", newRole.Id);

                if (role == null)
                {
                    throw new Exception("Role is not found!");
                }

                return db.Update(roleInstance.NameTable, role.toDataRow(), newRole.toDataRow());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
