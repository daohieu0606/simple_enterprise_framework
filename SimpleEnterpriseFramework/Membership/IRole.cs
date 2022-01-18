using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Membership_v2
{
    public abstract class IRole
    {
        private String nameTable = "role";
        private String id;

        public string Id { get => id; set => id = value; }
        public string NameTable { get => nameTable; set => nameTable = value; }

        public abstract IRole clone() ;
        public abstract DataRow toDataRow();
        public abstract DataTable makeRoleDataTable();
        public abstract IRole getInstance(DataRow dr);
    }
}
