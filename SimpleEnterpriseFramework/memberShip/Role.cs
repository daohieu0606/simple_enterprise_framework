namespace Membership
{
    using HelperLibrary;
    using Membership_v2;
    using System;
    using System.Data;

    public class Role : Membership_v2.IRole
    {
        private string roleName;

        public Role()
        {
            this.RoleName = "";
        }

        public string RoleName { get => roleName; set => roleName = value; }

        public static Role getInstance(string name, string id = "")
        {
            Role role = new Role();
            role.roleName = name;
            role.Id = id;
            return role;
        }
        override
        public IRole clone()
        {
            Role role = new Role();
            role.roleName = this.roleName;
            role.Id = this.Id;
            return role;
        }
        override
       public IRole getInstance(DataRow dr)
        {
            Console.WriteLine(dr["role_id"]);
            Role role = new Role();
            role.roleName = dr.Field<string>("rolename");
            role.Id = dr.Field<String>("role_id");
            return role;
        }
        override
       public DataTable makeRoleDataTable()
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "role_id", typeof(string).ToString());
            table = TableHelper.addColumn(table, "rolename", typeof(string).ToString());
            return table;
        }
        override
       public DataRow toDataRow()
        {
            DataTable dt = makeRoleDataTable();
            DataRow dr = dt.NewRow();
            dr["role_id"] = this.Id;
            dr["rolename"] = this.roleName;
            return dr;
        }
    }
}
