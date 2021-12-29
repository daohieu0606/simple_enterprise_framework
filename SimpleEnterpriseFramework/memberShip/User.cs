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
        
        public void addRole(Role role)
        {
            Role[] temp = new Role[roles.Length+1];

            for(int index = 0; index < roles.Length; index++)
            {
                temp[index] = Role.getInstance(roles[index].RoleName);
            }
            roles = temp;

        }
        public static User getInstance(string username, string password, string email = "", string phongNumber, string address, string role)
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
    }
}
