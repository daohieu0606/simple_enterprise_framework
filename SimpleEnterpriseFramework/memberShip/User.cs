using HelperLibrary;
using System;
using System.Data;

namespace Membership
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

        public static string nameTable = "accounts";

        public string Password { get => password; set => password = value; }

        public string Username { get => username; set => username = value; }

        public string Email { get => email; set => email = value; }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public string Address { get => address; set => address = value; }

        internal Role[] Roles { get => roles; set => roles = value; }

        public string Id { get => id; set => id = value; }

        public void addRole(Role role)
        {
            int length = 1;
            if (roles != null)
            {
                length = roles.Length + 1;
            }

            Role[] temp = new Role[length];

            for (int index = 0; index < length - 1; index++)
            {
                temp[index] = Role.getInstance(roles[index].RoleName);
            }
            temp[length - 1] = role;
            roles = temp;
        }

        public static User getInstance(string username, string password, string email, string phongNumber, string address, string role)
        {
            User user = new User();
            user.address = address;
            user.username = username;
            user.password = password;
            user.phoneNumber = phongNumber;
            user.email = email;
            Role _role = Role.getInstance(role);
            user.addRole(_role);
            return user;
        }

        public DataTable makeUserDataTable()
        {
            DataTable table = new DataTable();
            table = TableHelper.addColumn(table, "user_id", typeof(string).ToString());
            table = TableHelper.addColumn(table, "username", typeof(String).ToString());
            table = TableHelper.addColumn(table, "password", typeof(String).ToString());
            table = TableHelper.addColumn(table, "email", typeof(String).ToString());
            table = TableHelper.addColumn(table, "phonenumber", typeof(String).ToString());
            table = TableHelper.addColumn(table, "address", typeof(String).ToString());
            return table;
        }

        public DataRow parseToDataRow()
        {
            DataTable dtUser = makeUserDataTable();
            DataRow dt = dtUser.NewRow();
            dt["user_id"] = null;
            dt["username"] = this.username;
            dt["password"] = this.password;
            dt["email"] = this.email;
            dt["phonenumber"] = this.phoneNumber;
            dt["address"] = this.address;

            return dt;
        }

        public static User getInstance(DataRow dt)
        {
            if (dt == null)
            {
                return null;
            }
            User user = new User();
            if (dt == null) return user; ;
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
            table = TableHelper.addColumn(table, "user_id", typeof(String).ToString());
            table = TableHelper.addColumn(table, "role_id", typeof(String).ToString());
            DataRow dt = table.NewRow();
            dt["user_id"] = user_id;
            dt["role_id"] = role_id;
            return dt;
        }
    }
}
