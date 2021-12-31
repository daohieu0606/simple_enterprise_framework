using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemberShip
{
    class Authentication
    {
        private static int salt = 12;
        public static string Hash(string value)
        {
            if (value == null)
            {
                return "";
            }
            return BCrypt.Net.BCrypt.HashPassword(value, salt);
        }

        public static async Task<bool> validateAsync(string username, string password)
        {
            User user = await HandleUser.findOneUserByFieldAsync("username", username);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

    }
}
