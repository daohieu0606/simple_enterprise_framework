namespace Membership
{
    using HelperLibrary;
    using System;
    using System.Data;

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

        public static Role getInstance(Role _role)
        {
            Role role = new Role();
            role.roleName = _role.roleName;
            role.id = _role.Id;
            return role;
        }

        public static Role getInstance(DataRow dr)
        {
            Console.WriteLine(dr["role_id"]);
            Role role = new Role();
            role.roleName = dr.Field<string>("rolename");
            role.id = dr.Field<String>("role_id");
            return role;
        }

        public DataTable makeRoleDataTable()
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "role_id", typeof(string).ToString());
            table = TableHelper.addColumn(table, "rolename", typeof(string).ToString());
            return table;
        }

        public DataRow toDataRow()
        {
            DataTable dt = makeRoleDataTable();
            DataRow dr = dt.NewRow();
            dr["role_id"] = this.id;
            dr["rolename"] = this.roleName;
            return dr;
        }
    }
}
