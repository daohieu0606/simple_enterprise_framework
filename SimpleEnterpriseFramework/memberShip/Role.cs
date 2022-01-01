using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MemberShip
{
    public class Role
    {
        private string id;
        private string roleName;
        public static string nameTable = "role";
        public Role()
        {
            this.RoleName = "";
        }
        public string RoleName { get => roleName; set => roleName = value; }
        public string Id { get => id; set => id = value; }

        public static Role getInstance(string name, string id = "")
        {
            Role role = new Role();
            role.roleName = name;
            role.id = id;
            return role;
        }

        public static Role getInstance(DataRow dr)
        {
            Role role = new Role();
            role.roleName = dr.Field<string>("rolename");
            role.id = dr.Field<String>("role_id");
            return role;
        }
        public DataTable makeRoleDataTable()
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "role_id", "System.string");
            table = TableHelper.addColumn(table, "rolename", "System.string");
            return table;
        }
        public DataRow toDataRow()
        {
            DataTable dt = makeRoleDataTable();
            return dt.NewRow();
        }
    }
}
