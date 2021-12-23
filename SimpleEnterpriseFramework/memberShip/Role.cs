using System;
using System.Collections.Generic;
using System.Text;

namespace MemberShip
{
    class Role
    {
        private string roleName;

        public Role()
        {
            this.RoleName = "";
        }
        public string RoleName { get => roleName; set => roleName = value; }
    }
}
