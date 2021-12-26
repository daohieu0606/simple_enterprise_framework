using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class User
    {
        private string id;
        private string username;
        private string password;
        private string email;
        private string phoneNumber;
        private string address;
        private Role[] roles;
        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
        internal Role[] Roles { get => roles; set => roles = value; }
        public string Id { get => id; set => id = value; }

        public void save()
        {

        }
        public static User getInstance(string username, string password, string email = "", string phongNumber, string address, string role)
        {
            return new User();
        }
    }
}
