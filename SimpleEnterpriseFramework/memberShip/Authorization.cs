using Core.Database;
using IoC.DI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        public static bool AddRolesToUser(Role[] roles,  string id)
        {
            bool isSuccess = true;
            foreach(Role role in roles)
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
        public static bool RemoveRoleFromUser(Role role,  string id)
        {
            try
            {
                return db.Delete(nameTable, User.toUserRoleDataRow(role.Id, id));
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public static Role[] getAllRoles()
        {
            DataTable dt = db.GetTable(Role.nameTable);
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

                foreach(Role item in roles)
                {
                    if(role.Id == item.Id)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public static Role findRoleByName(string roleName)
        {
           
            try
            {
                DataRow dt = db.GetOneRow(Role.nameTable, "rolename", roleName);

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
        public static Role findRoleField(string field, string value)
        {

            try
            {
                DataRow dt = db.GetOneRow(Role.nameTable, field, value);

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

        public static Role createRole(Role role)
        {
            try
            {
                bool isSuccess = db.Insert(Role.nameTable, role.toDataRow());
                if (!isSuccess)
                {
                    return null;
                }
                Role _role = findRoleByName(role.RoleName);
                return _role;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public static bool removeRole(string id)
        {
            try
            {
                Role role = findRoleField("role_id", id);
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
        public static bool updateRole( Role newRole)
        {
            try
            {
                Role role = findRoleByName(newRole.RoleName);

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
