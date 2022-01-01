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
    class Authorization
    {
        private static IDatabase db = ServiceLocator.Instance.Get<IDatabase>();
        public static string nameTable = "user_role";
        public static bool AddRoleToUser(Role role, string id)
        {
            try
            {
                return db.Insert(nameTable, User.toUserRoleDataRow(role.Id, id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool AddRolesToUser(Role[] roles, string id)
        {
            bool isSuccess = true;
            foreach (Role role in roles)
            {
                try
                {
                    isSuccess = db.Insert(nameTable, User.toUserRoleDataRow(role.Id, id));
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
        public static bool AddRoleToUsers(Role role, string[] ids)
        {
            bool isSuccess = true;
            foreach (string id in ids)
            {
                try
                {
                    isSuccess = db.Insert(nameTable, User.toUserRoleDataRow(role.Id, id));
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
        public static bool AddRolesToUsers(Role[] roles, string[] ids)
        {
            bool isSuccess = true;
            foreach (string id in ids)
            {
                try
                {
                    isSuccess = AddRolesToUser(roles, id);
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
        public static bool RemoveRoleFromUser(Role role, string id)
        {
            try
            {
                return db.Delete(nameTable, User.toUserRoleDataRow(role.Id, id));
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
        public static Role[] GetRolesOfUser(User user)
        {
            //db.findDataFrom()--wating for new API
            return null;
        }

        public static bool isUserInRole(User user, Role role)
        {
            try
            {
                Role[] roles = GetRolesOfUser(user);

                foreach (Role item in roles)
                {
                    if (role.Id == item.Id)
                    {
                        return true;
                    }
                }
                return false;
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
                DataRow dt = await db.GetOneRow(Role.nameTable, "rolename", roleName);

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
        public static async Task<Role> findRoleFieldAsync(string field, string value)
        {

            try
            {
                DataRow dt = await db.GetOneRow(Role.nameTable, field, value);

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

                return db.Delete(nameTable, role.toDataRow());

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
                Role role = await findRoleByNameAsync(newRole.RoleName);

                if (role == null)
                {
                    throw new Exception("Role is not found!");
                }

                return db.Update(nameTable, role.toDataRow(), newRole.toDataRow());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
