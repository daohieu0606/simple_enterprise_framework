using HelperLibrary;
using Membership_v2;
using System;
using System.Data;

namespace Membership
{
    public class User : IUser 
    {

        private string username;

       

        private string email;

        private string phoneNumber;

        private string address;



        public string Username { get => username; set => username = value; }

        public string Email { get => email; set => email = value; }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public string Address { get => address; set => address = value; }

        public static User getInstance(string username, string password, string email, string phongNumber, string address)
        {
            User user = new User();
            user.address = address;
            user.username = username;
            user.Password = password;
            user.phoneNumber = phongNumber;
            user.email = email;
            return user;
        }
        override
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
        override
        public DataRow parseToDataRow()
        {
            DataTable dtUser = makeUserDataTable();
            DataRow dt = dtUser.NewRow();
            dt["user_id"] = null;
            dt["username"] = this.username;
            dt["password"] = this.Password;
            dt["email"] = this.email;
            dt["phonenumber"] = this.phoneNumber;
            dt["address"] = this.address;

            return dt;
        }
        override
        public IUser getInstance(DataRow dt)
        {
            if (dt == null)
            {
                return null;
            }
            User user = new User();
            if (dt == null) return user; ;
            user.username = dt.Field<string>("username");
            user.Password = dt.Field<string>("password");
            user.email = dt.Field<string>("email");
            user.phoneNumber = dt.Field<string>("phonenumber");
            user.address = dt.Field<string>("address");

            return user;
        }
        override
        public IUser clone()
        {
            User copyUser = new User();
            copyUser.Id = this.Id;
            copyUser.username = this.username;
            copyUser.Password = this.Password;
            copyUser.email = this.email;
            copyUser.phoneNumber = this.phoneNumber;
            copyUser.address = this.address;

            return copyUser;
        }


    }
}
