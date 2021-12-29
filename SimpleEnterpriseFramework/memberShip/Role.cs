using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Role
    {
        private string id;
        private string roleName;

        private Role()
        {
            this.RoleName = "";
        }
        public string RoleName { get => roleName; set => roleName = value; }
        public string Id { get => id; set => id = value; }

        public static Role getInstance(string name, string id="")
        {
            Role role = new Role();
            role.roleName = name;
            role.id = id;
            return role;
        }
    }
}
