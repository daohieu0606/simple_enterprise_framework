using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Model
{
    public class UserModel : IEquatable<UserModel>
    {
        public string name
        {
            get; set;
        }

        public string password
        {
            get; set;
        }

        public UserModel()
        {
        }

        public override string ToString()
        {
            return "Nombre: " + name + " Password: " + password;
        }

        public UserModel(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public bool Equals(UserModel other)
        {
            if (other == null) return false;
            return (this.name.Equals(other.name) && this.password.Equals(other.password));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            UserModel user = obj as UserModel;
            if (user == null) return false;
            else return Equals(user);
        }
    }
}
