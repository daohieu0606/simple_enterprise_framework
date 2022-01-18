using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Membership_v2
{
    public abstract class IUser
    {
        private string id;
        private string nameTable = "accounts";
        private string password;
        public string Id { get => id; set => id = value; }
        public string NameTable { get => nameTable; set => nameTable = value; }

        public string Password { get => password; set => password = value; }
        public abstract IUser getInstance(DataRow dt);
        public abstract IUser clone();
        public abstract DataRow parseToDataRow();
        public abstract DataTable makeUserDataTable();
        public DataRow toUserRoleDataRow(string user_id, string role_id)
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "user_id", typeof(String).ToString());
            table = TableHelper.addColumn(table, "role_id", typeof(String).ToString());
            DataRow dt = table.NewRow();
            dt["user_id"] = user_id;
            dt["role_id"] = role_id;
            return dt;
        }
    }
}