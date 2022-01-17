using System;
using System.Collections.Generic;
using System.Text;

namespace Membership
{
    public class Role
    {
        private string roleName;

        public Role()
        {
            this.RoleName = "";
        }
        public string RoleName { get => roleName; set => roleName = value; }
    }
}
