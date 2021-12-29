using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MemberShip
{
    public class User
    {
        private string id;
        private string username;
        private string password;
        private string email;
        private string phoneNumber;
        private string address;
        private Role[] roles;
        public static string nameTable;
        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
        internal Role[] Roles { get => roles; set => roles = value; }
        public string Id { get => id; set => id = value; }

        public void addRole(Role role)
        {
            Role[] temp = new Role[roles.Length+1];

            for(int index = 0; index < roles.Length; index++)
            {
                temp[index] = Role.getInstance(roles[index].RoleName);
            }
            roles = temp;

        }
        public static User getInstance(string username, string password, string email, string phongNumber, string address, string role)
        {
            User user = new User();
            user.address = address;
            user.username = username;
            user.phoneNumber = phongNumber;
            user.email = email;
            Role _role = Role.getInstance(role);
            user.addRole(_role);
            return user;
        }
        public DataTable makeUserDataTable()
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "user_id", "System.string");
            table = TableHelper.addColumn(table, "email", "System.string");
            table = TableHelper.addColumn(table, "username", "System.string");
            table = TableHelper.addColumn(table, "password", "System.string");
            table = TableHelper.addColumn(table, "phonenumber", "System.string");
            table = TableHelper.addColumn(table, "address", "System.string");
            return table;
        }
        public DataRow parseToDataRow()
        {
            DataTable dtUser = makeUserDataTable();
            DataRow dt = dtUser.NewRow();
            dt["user_id"] = this.id;
            dt["username"] = this.username;
            dt["password"] = this.password;
            dt["email"] = this.email;
            dt["phonenumber"] = this.phoneNumber;
            dt["address"] = this.address;

            return dt;
        }
        public static User getInstance(DataRow dt)
        {
            User user = new User();
            user.id = dt.Field<string>("user_id");
            user.username = dt.Field<string>("username");
            user.password = dt.Field<string>("password");
            user.email = dt.Field<string>("email");
            user.phoneNumber = dt.Field<string>("phonenumber");
            user.address = dt.Field<string>("address");

            return user;
        }
        public static User copy(User user)
        {
            User copyUser = new User();
            copyUser.id = user.id;
            copyUser.username = user.username;
            copyUser.password = user.password;
            copyUser.email = user.email;
            copyUser.phoneNumber = user.phoneNumber;
            copyUser.address = user.address;

            return copyUser;
        }
        public static DataRow toUserRoleDataRow(string user_id, string role_id)
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "user_id", "System.string");
            table = TableHelper.addColumn(table, "role_id", "System.string");
            DataRow dt = table.NewRow();
            dt["user_id"] = user_id;
            dt["role_id"] = role_id;
            return dt;
        }
    }
}
