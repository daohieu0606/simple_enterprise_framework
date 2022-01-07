namespace MemberShip
{
    class Authorization
    {
        public bool AddRoleToUser(Role role, User user)
        {
            return true;
        }
        public bool AddRolesToUser(Role[] role, User user)
        {
            return true;
        }
        public bool AddRoleToUsers(Role role, User[] user)
        {
            return true;
        }
        public bool AddRolesToUsers(Role[] role, User[] user)
        {
            return true;
        }
        public bool RemoveRoleFromUser(Role role, User user)
        {
            return true;
        }
        public Role[] getAllRole()
        {
            return null;
        }
        public User[] GetUsersInRole(Role role)
        {
            return null;
        }

        public bool isUserInRole(Role role)
        {
            return true;
        }
    }
}
