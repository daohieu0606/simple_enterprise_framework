namespace Membership
{
    /// <summary>
    /// Defines the <see cref="User" />.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Defines the username.
        /// </summary>
        private string username;

        /// <summary>
        /// Defines the password.
        /// </summary>
        private string password;

        /// <summary>
        /// Defines the email.
        /// </summary>
        private string email;

        /// <summary>
        /// Defines the phoneNumber.
        /// </summary>
        private string phoneNumber;

        /// <summary>
        /// Defines the address.
        /// </summary>
        private string address;

        //private Role[] roles;
        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { get => password; set => password = value; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get => username; set => username = value; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get => email; set => email = value; }

        /// <summary>
        /// Gets or sets the PhoneNumber.
        /// </summary>
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address { get => address; set => address = value; }
    }
}
